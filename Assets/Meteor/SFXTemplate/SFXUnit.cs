﻿using UnityEngine;
using System.Collections;

//播放一个sfxfile中的一个子特效
//自己追踪目标的位置和旋转。
//子Mesh设置自己的本地坐标和旋转。
[System.Serializable]
public class SFXUnit : MonoBehaviour
{
    public SFXEffectPlay parentSfx;
    public int effectIndex;
    public Texture tex;
    public SfxEffect source;
    public string EffectType;
    MeshRenderer mRender;
    MeshFilter mFilter;
    public MeteorUnit mOwner;
    public Collider damageBox;
    public Transform PositionFollow;//同步移动
    public Transform RotateFollow;//同步旋转，若没有，则不用跟随旋转
    public string texture;
    //public Mesh[] mesh;
    public ParticleSystem particle;
    public IFLLoader iflTexture;
    Coroutine playCoroutine;
    public bool PlayDone = false;
    public bool pause = false;
    bool LookAtC = false;
    void Awake()
    {
    }
    /* BOX 立体模型、
AUDIO 声音指向、Audio_3音效 Audio_0 UI音效 Audio_15 多一个字节？
PLANE 平面模型、
DONUT 圆环模型、
MODEL 物品模型、
SPHERE 球体模型、
PARTICLE 颗粒模型、
CYLINDER 圆柱模型、
BILLBOARD 连接模板、公告板
DRAG*/
    //第一个参数 控制层级关系
    //第二个参数，本地坐标系跟随，
    public void OnDestroy()
    {
        if (mOwner != null)
            mOwner.OnSFXDestroy(this);
    }

    public void Hide()
    {
        if (mRender != null)
            mRender.enabled = false;
    }

    //原粒子系统是右手坐标系的，转移到左手坐标系后非常多问题，特别是父子关系以及，跟随和子物体旋转叠加
    public void Init(SfxEffect effect, SFXEffectPlay parent, int index, float timePlayed = 0.0f)
    {
        playedTime = timePlayed;
        PlayDone = false;
        transform.rotation = Quaternion.identity;
        effectIndex = index;
        parentSfx = parent;//当找不到跟随者的时候，向父询问其他特效是否包含此名称.
        name = effect.EffectName;
        EffectType = effect.EffectType;
            //一个是跟随移动，一个是跟随旋转
        mOwner = parent.GetComponent<MeteorUnit>();
        source = effect;
        if (effect.EffectType == "PARTICLE")
            particle = Instantiate(Resources.Load<GameObject>("SFXParticles"), Vector3.zero, Quaternion.identity, transform).GetComponent<ParticleSystem>();

        if (string.IsNullOrEmpty(effect.Bone0))
            PositionFollow = mOwner == null ? parentSfx.transform : mOwner.transform;
        else
        if (effect.Bone0 == "ROOT")
            PositionFollow = mOwner == null ? parent.transform : mOwner.ROOTNull;
        else if (effect.Bone0 == "Character")//根骨骼上一级，角色，就是不随着b骨骼走，但是随着d_base走
            PositionFollow = mOwner == null ? parent.transform: mOwner.RootdBase;
        else
            PositionFollow = FindFollowed(effect.Bone0);

        if (string.IsNullOrEmpty(effect.Bone1))
            RotateFollow = mOwner == null ? parent.transform : mOwner.transform;
        else
        if (effect.Bone1 == "ROOT")
            RotateFollow = mOwner == null ? parent.transform : mOwner.ROOTNull;
        else if (effect.Bone1 == "Character")
            RotateFollow = mOwner == null ? parent.transform : mOwner.RootdBase;
        else
            RotateFollow = FindFollowed(effect.Bone1);

        if (PositionFollow != null)
            transform.position = PositionFollow.transform.position;

        mRender = gameObject.GetComponent<MeshRenderer>();
        mFilter = gameObject.GetComponent<MeshFilter>();
        int meshIndex = -1;

        if (effect.EffectType == "DONUT")
        {
            //Debug.LogError("find Donut Effect");
        }
        //部分模型是要绕X旋转270的，这样他的缩放，和移动，都只能靠自己
        if (effect.EffectType == "PLANE")
        {
            meshIndex = 0;
        }
        else if (effect.EffectType == "BOX")
        {
            meshIndex = 1;
        }
        else if (effect.EffectType == "DONUT")
        {
            transform.localScale = effect.frames[0].scale;
            meshIndex = 2;
        }
        else if (effect.EffectType == "MODEL")//自行加载模型
        {
            transform.localScale = effect.frames[0].scale;
            transform.rotation = RotateFollow.rotation * effect.frames[0].quat;
            transform.position = PositionFollow.position + effect.frames[0].pos;
            if (!string.IsNullOrEmpty(effect.Tails[0]))
            {
                string[] des = effect.Tails[0].Split(new char[] { '\\' }, System.StringSplitOptions.RemoveEmptyEntries);
                if (des.Length != 0)
                    WsGlobal.ShowMeteorObject(des[des.Length - 1], transform);
            }
            //这个时候,不要用自带的meshfilter了,让他自己处理显示问题,只要告诉他在哪个地方显示
            meshIndex = 100;
        }
        else if (effect.EffectType == "SPHERE")
        {
            meshIndex = 3;
        }
        else if (effect.EffectType == "PARTICLE")
        {
            if (!string.IsNullOrEmpty(effect.Tails[0]))
                PositionFollow = FindFollowed(effect.Tails[0]);
            if (!string.IsNullOrEmpty(effect.Tails[1]))
                RotateFollow = FindFollowed(effect.Tails[1]);
            if (PositionFollow != null)
                transform.position = PositionFollow.transform.position;
            meshIndex = 101;
        }
        else if (effect.EffectType == "CYLINDER")
        {
            meshIndex = 4;
        }
        else if (effect.EffectType == "BILLBOARD")
        {
            LookAtC = true;
            meshIndex = 5;
        }
        else if (effect.EffectType == "DRAG")
        {
            meshIndex = 6;
        }
        //决定模型
        if (meshIndex != -1 && meshIndex < Game.Instance.MeshMng.Meshes.Length)
        {
            if (meshIndex == 4)
                mFilter.mesh = SfxMeshGenerator.Instance.CreateCylinder(source.origAtt.y, source.origAtt.x, source.origScale.x);
            else if (meshIndex == 3)
                mFilter.mesh = SfxMeshGenerator.Instance.CreateSphere(source.SphereScale);
            else if (meshIndex == 0)
                mFilter.mesh = SfxMeshGenerator.Instance.CreatePlane(source.origScale.x, source.origScale.y);
            else
                mFilter.mesh = Game.Instance.MeshMng.Meshes[meshIndex];
        }
        if (effect.Texture.ToLower().EndsWith(".bmp") || effect.Texture.ToLower().EndsWith(".jpg") || effect.Texture.ToLower().EndsWith(".tga"))
        {
            texture = effect.Texture.Substring(0, effect.Texture.Length - 4);
            tex = Resources.Load<Texture>(texture);
        }
        else if (effect.Texture.ToLower().EndsWith(".ifl"))
        {
            iflTexture = gameObject.AddComponent<IFLLoader>();
            iflTexture.IFLFile = Resources.Load<TextAsset>(effect.Texture);
            if (effect.EffectType == "PARTICLE")
                iflTexture.SetTargetMeshRenderer(particle.GetComponent<ParticleSystemRenderer>());
            iflTexture.LoadIFL();//传递false表示由特效控制每一帧的切换时间.
            tex = iflTexture.GetTexture(0);
        }
        //else
        //    print("effect contains other prefix:" + effect.Texture == null ? " texture is null" : effect.Texture);
        if (tex != null)
        {
            if (effect.EffectType == "PARTICLE")
            {
                ParticleSystemRenderer render = particle.GetComponent<ParticleSystemRenderer>();
                if (effect.BlendType == 0)
                    render.material.shader = Shader.Find("Unlit/Texture");//普通不透明无光照
                else
                if (effect.BlendType == 1)
                    render.material.shader = Shader.Find("UnlitAdditive");//滤色 加亮 不透明
                else
                if (effect.BlendType == 2)
                    render.material.shader = Shader.Find("UnlitAdditive");//反色+透明度
                else if (effect.BlendType == 3)
                    render.material.shader = Shader.Find("Mobile/Particles/Alpha Blended");//不滤色,支持透明
                else if (effect.BlendType == 4)
                    render.material.shader = Shader.Find("Custom/MeteorBlend4");//滤色，透明
                else
                    render.material.shader = Shader.Find("Unlit/Texture");//普通不透明无光照
                render.material.SetTexture("_MainTex", tex);
                render.material.SetColor("_Color", effect.frames[0].colorRGB);
                render.material.SetColor("_TintColor", effect.frames[0].colorRGB);
                render.material.SetFloat("_Intensity", effect.frames[0].TailFlags[9]);
                //particle.Emit(1);
            }
            else
            {
                if (effect.BlendType == 0)
                    mRender.material.shader = Shader.Find("Unlit/Texture");//普通不透明无光照
                else
                if (effect.BlendType == 1)
                    mRender.material.shader = Shader.Find("UnlitAdditive");//滤色 加亮 不透明
                else
                if (effect.BlendType == 2)
                    mRender.material.shader = Shader.Find("UnlitAdditive");//反色+透明度
                else if (effect.BlendType == 3)
                    mRender.material.shader = Shader.Find("Mobile/Particles/Alpha Blended");//不滤色,支持透明
                else if (effect.BlendType == 4)
                    mRender.material.shader = Shader.Find("Custom/MeteorBlend4");//滤色，透明
                else
                    mRender.material.shader = Shader.Find("Unlit/Texture");//普通不透明无光照

                if (effect.BlendType == 2)
                {
                    mRender.material.SetColor("_InvColor", effect.frames[0].colorRGB);
                }
                else
                {
                    mRender.material.SetColor("_Color", effect.frames[0].colorRGB);
                    mRender.material.SetColor("_TintColor", effect.frames[0].colorRGB);
                }
                mRender.material.SetFloat("_Intensity", effect.frames[0].TailFlags[9]);
                mRender.material.SetTexture("_MainTex", tex);
                if (effect.uSpeed != 0.0f || effect.vSpeed != 0.0f)
                    tex.wrapMode = TextureWrapMode.Repeat;
                else
                    tex.wrapMode = TextureWrapMode.Clamp;
                mRender.material.SetFloat("_u", effect.uSpeed);
                mRender.material.SetFloat("_v", effect.vSpeed);
            }
        }

        //drag不知道有什么作用，可能只是定位用的挂载点
        if (effect.Hidden == 1 || meshIndex == 6)
        {
            if (particle != null)
            {
                ParticleSystemRenderer render = particle.GetComponent<ParticleSystemRenderer>();
                if (render != null)
                    render.enabled = false;
            }
            mRender.enabled = false;
        }
        if (effect.EffectName.StartsWith("Attack"))
        {
            MeshCollider co = gameObject.AddComponent<MeshCollider>();
            co.convex = true;
            co.isTrigger = true;
            damageBox = co;
            //if (effect.EffectType == "SPHERE")
            //{
            //    SphereCollider sph = gameObject.AddComponent<SphereCollider>();
            //    damageBox = sph;
            //    //sph.radius = 1.0f;
            //    //sph.center = Vector3.zero;
            //}
            //else
            //{
            //    BoxCollider bo = gameObject.AddComponent<BoxCollider>();
            //    //bo.center = Vector3.zero;
            //    //bo.size = Vector3.one;
            //    damageBox = bo;
            //}
            damageBox.enabled = false;
        }

        playCoroutine = StartCoroutine(PlayEffectFrame());
    }

    public Transform FindFollowed(string bone)
    {
        GameObject bindBone = Global.ldaControlX(bone, parentSfx.gameObject);
        if (bindBone != null)
            return bindBone.transform;
        //不能跨特效搜索，只能在本特效中
        Transform tr = parentSfx.FindEffectByName(bone);
        if (tr != null)
            bindBone = tr.gameObject;
        return bindBone == null ? null : bindBone.transform;
    }

    void Start()
    {

    }

    float playedTime = 0.0f;
    int playedIndex = 1;
    // Update is called once per frame
    void Update()
    {
        playedTime += Time.deltaTime;
        //if (LookAtC)
        //    transform.LookAt(Camera.main.transform);
    }

    //重复播放
    public void RePlay()
    {
        playedTime = 0.0f;
        playedIndex = 1;
        PlayDone = false;
        if (playCoroutine != null)
            StopCoroutine(playCoroutine);
        playCoroutine = StartCoroutine(PlayEffectFrame());
    }

    IEnumerator PlayEffectFrame()
    {
        while (playedIndex < source.frames.Count)
        {
            //某帧锁定
            if (pause)
            {
                yield return 0;
                continue;
            }
            if (playedTime < source.frames[0].startTime && mRender.enabled)
                mRender.enabled = false;
            else if (playedTime >= source.frames[0].startTime && !mRender.enabled && source.Hidden == 0 && EffectType != "DRAG")
                mRender.enabled = true;

            while (playedTime < source.frames[0].startTime)
                yield return 0;
            ChangeAttackCore();
            float timeRatio2 = (playedTime - source.frames[playedIndex - 1].startTime) / (source.frames[playedIndex].startTime - source.frames[playedIndex - 1].startTime);
            
            string vertexColor = "_TintColor";
            if (source.BlendType == 2)
                vertexColor = "_InvColor";

            if (source.EffectType == "BOX")
            {
                if (source.frames[playedIndex].startTime <= playedTime)
                {
                    OnNewFrame(vertexColor);
                    transform.localScale = new Vector3(source.origScale.x * source.frames[playedIndex-1].scale.x, source.origScale.y * source.frames[playedIndex-1].scale.y, source.origScale.z * source.frames[playedIndex-1].scale.z);
                }
                else
                {
                    OnLastFrame(timeRatio2, vertexColor);
                    transform.localScale = Vector3.Lerp(new Vector3(source.origScale.x * source.frames[playedIndex - 1].scale.x, source.origScale.y * source.frames[playedIndex - 1].scale.y, source.origScale.z * source.frames[playedIndex - 1].scale.z), new Vector3(source.origScale.x * source.frames[playedIndex].scale.x, source.origScale.y * source.frames[playedIndex].scale.y, source.origScale.z * source.frames[playedIndex].scale.z), timeRatio2);
                }
            }
            else if (source.EffectType == "CYLINDER")
            {
                if (source.frames[playedIndex].startTime <= playedTime)
                {
                    OnNewFrame(vertexColor);
                    transform.localScale = source.frames[playedIndex-1].scale;
                }
                else
                {
                    OnLastFrame(timeRatio2, vertexColor);
                    transform.localScale = Vector3.Lerp(source.frames[playedIndex - 1].scale, source.frames[playedIndex].scale, timeRatio2);
                }
                //if (source.frames[playedIndex].startTime <= playedTime)
                //{
                //    if (RotateFollow != null)
                //        transform.rotation = RotateFollow.rotation * source.frames[playedIndex].quat;
                //    else
                //        transform.rotation = source.frames[playedIndex].quat;

                //    if (PositionFollow != null)
                //        transform.position = PositionFollow.position + source.frames[playedIndex].pos;
                //    else
                //        transform.position = source.frames[playedIndex].pos;
                //    transform.localScale = source.frames[playedIndex].scale;
                //    mRender.material.SetFloat("_Intensity", source.frames[playedIndex].TailFlags[9]);
                //    mRender.material.SetColor(vertexColor, source.frames[playedIndex].colorRGB);
                //    playedIndex++;
                //}
                //else
                //{
                //    if (RotateFollow != null)
                //        transform.rotation = RotateFollow.rotation * Quaternion.Slerp(source.frames[playedIndex - 1].quat, source.frames[playedIndex].quat, timeRatio2);
                //    else
                //        transform.rotation = Quaternion.Slerp(source.frames[playedIndex - 1].quat, source.frames[playedIndex].quat, timeRatio2);
                //    if (PositionFollow != null)
                //        transform.position = PositionFollow.position + Vector3.Lerp(source.frames[playedIndex - 1].pos, source.frames[playedIndex].pos, timeRatio2);
                //    else
                //        transform.position = Vector3.Lerp(source.frames[playedIndex - 1].pos, source.frames[playedIndex].pos, timeRatio2);
                //    transform.localScale = Vector3.Lerp(source.frames[playedIndex - 1].scale, source.frames[playedIndex].scale, timeRatio2);
                    
                //    mRender.material.SetFloat("_Intensity", Mathf.Lerp(source.frames[playedIndex - 1].TailFlags[9], source.frames[playedIndex].TailFlags[9], timeRatio2));
                //    mRender.material.SetColor(vertexColor, Color.Lerp(source.frames[playedIndex - 1].colorRGB, source.frames[playedIndex].colorRGB, timeRatio2));
                //}
            }
            else if (source.EffectType == "PLANE")
            {
                if (source.frames[playedIndex].startTime <= playedTime)
                {
                    OnNewFrame(vertexColor);
                    transform.localScale = source.frames[playedIndex-1].scale;
                }
                else
                {
                    OnLastFrame(timeRatio2, vertexColor);
                    transform.localScale = Vector3.Lerp(source.frames[playedIndex - 1].scale, source.frames[playedIndex].scale, timeRatio2);
                }
                //if (source.frames[playedIndex].startTime <= playedTime)
                //{
                //    if (RotateFollow != null)
                //        transform.rotation = RotateFollow.rotation * source.frames[playedIndex].quat; 
                //    else
                //        transform.rotation = source.frames[playedIndex].quat;

                //    if (PositionFollow != null)
                //        transform.position = PositionFollow.position + source.frames[playedIndex].pos;
                //    else
                //        transform.position = source.frames[playedIndex].pos;

                //    transform.localScale = new Vector3(source.origScale.x * source.frames[playedIndex].scale.x, source.origScale.y * source.frames[playedIndex].scale.y, source.origScale.z * source.frames[playedIndex].scale.z);
                //    mRender.material.SetColor(vertexColor, source.frames[playedIndex].colorRGB);
                //    mRender.material.SetFloat("_Intensity", source.frames[playedIndex].TailFlags[9]);
                //    playedIndex++;
                //}
                //else
                //{
                //    if (RotateFollow != null)
                //        transform.rotation = RotateFollow.rotation * Quaternion.Slerp(source.frames[playedIndex - 1].quat, source.frames[playedIndex].quat, timeRatio2);
                //    else
                //        transform.rotation = Quaternion.Slerp(source.frames[playedIndex - 1].quat, source.frames[playedIndex].quat, timeRatio2);
                //    if (PositionFollow != null)
                //        transform.localPosition = PositionFollow.position + Vector3.Lerp(source.frames[playedIndex - 1].pos, source.frames[playedIndex].pos, timeRatio2);
                //    else
                //        transform.position = Vector3.Lerp(source.frames[playedIndex - 1].pos, source.frames[playedIndex].pos, timeRatio2);
                //    transform.localScale = Vector3.Lerp(new Vector3(source.origScale.x * source.frames[playedIndex - 1].scale.x, source.origScale.y * source.frames[playedIndex - 1].scale.y, source.origScale.z * source.frames[playedIndex - 1].scale.z), new Vector3(source.origScale.x * source.frames[playedIndex].scale.x, source.origScale.y * source.frames[playedIndex].scale.y, source.origScale.z * source.frames[playedIndex].scale.z), timeRatio2);

                //    mRender.material.SetFloat("_Intensity", Mathf.Lerp(source.frames[playedIndex - 1].TailFlags[9], source.frames[playedIndex].TailFlags[9], timeRatio2));
                //    mRender.material.SetColor(vertexColor, Color.Lerp(source.frames[playedIndex - 1].colorRGB, source.frames[playedIndex].colorRGB, timeRatio2));
                //}
            }
            else if (source.EffectType == "SPHERE")
            {
                if (source.frames[playedIndex].startTime <= playedTime)
                {
                    OnNewFrame(vertexColor);
                    transform.localScale = source.frames[playedIndex-1].scale;
                }
                else
                {
                    OnLastFrame(timeRatio2, vertexColor);
                    transform.localScale = Vector3.Slerp(source.frames[playedIndex - 1].scale, source.frames[playedIndex].scale, timeRatio2);
                }
                //if (source.frames[playedIndex].startTime <= playedTime)
                //{
                //    if (RotateFollow != null)
                //        transform.rotation = RotateFollow.rotation * source.frames[playedIndex].quat;
                //    else
                //        transform.rotation = source.frames[playedIndex].quat;

                //    if (source.localSpace == 1)
                //    {
                //        if (RotateFollow != null)
                //        {
                //            Vector3 targetPos = RotateFollow.TransformPoint(source.frames[playedIndex].pos) - RotateFollow.position;
                //            if (PositionFollow != null)
                //                transform.position = PositionFollow.position + targetPos;
                //            else
                //                transform.position = targetPos;
                //        }
                //    }
                //    else
                //    {
                //        if (PositionFollow != null)
                //            transform.position = PositionFollow.position + source.frames[playedIndex].pos;
                //        else
                //            transform.position = source.frames[playedIndex].pos;
                //    }

                //    //if (PositionFollow != null)
                //    //    transform.position = PositionFollow.position + source.frames[playedIndex].pos;
                //    //else
                //    //    transform.position = source.frames[playedIndex].pos;
                //    transform.localScale = source.frames[playedIndex].scale;
                //    mRender.material.SetFloat("_Intensity", source.frames[playedIndex].TailFlags[9]);
                //    mRender.material.SetColor(vertexColor, source.frames[playedIndex].colorRGB);
                //    playedIndex++;
                //}
                //else
                //{
                //    //旋转用球面插值
                //    if (RotateFollow != null)
                //        transform.rotation = RotateFollow.rotation * Quaternion.Slerp(source.frames[playedIndex - 1].quat, source.frames[playedIndex].quat, timeRatio2);
                //    else
                //        transform.rotation = Quaternion.Slerp(source.frames[playedIndex - 1].quat, source.frames[playedIndex].quat, timeRatio2);

                //    if (source.localSpace == 1)
                //    {
                //        if (RotateFollow != null)
                //        {
                //            Vector3 targetPos = RotateFollow.TransformPoint(Vector3.Lerp(source.frames[playedIndex - 1].pos, source.frames[playedIndex].pos, timeRatio2)) - RotateFollow.position;
                //            if (PositionFollow != null)
                //                transform.position = PositionFollow.position + targetPos;
                //            else
                //                transform.position = targetPos;
                //        }
                //    }
                //    else
                //    {
                //        if (PositionFollow != null)
                //            transform.position = PositionFollow.position + Vector3.Lerp(source.frames[playedIndex - 1].pos, source.frames[playedIndex].pos, timeRatio2);
                //        else
                //            transform.position = Vector3.Lerp(source.frames[playedIndex - 1].pos, source.frames[playedIndex].pos, timeRatio2);
                //    }

                //    ////移动用直线插值，
                //    //if (PositionFollow != null)
                //    //    transform.position = PositionFollow.position + Vector3.Lerp(source.frames[playedIndex - 1].pos, source.frames[playedIndex].pos, timeRatio2);
                //    //else
                //    //    transform.localPosition = Vector3.Lerp(source.frames[playedIndex - 1].pos , source.frames[playedIndex].pos, timeRatio2);
                //    transform.localScale = Vector3.Slerp(source.frames[playedIndex - 1].scale, source.frames[playedIndex].scale, timeRatio2);
                //    mRender.material.SetFloat("_Intensity", Mathf.Lerp(source.frames[playedIndex - 1].TailFlags[9], source.frames[playedIndex].TailFlags[9], timeRatio2));
                //    mRender.material.SetColor(vertexColor, Color.Lerp(source.frames[playedIndex - 1].colorRGB, source.frames[playedIndex].colorRGB, timeRatio2));
                //}
            }
            else if (source.EffectType == "BILLBOARD")
            {
                if (source.frames[playedIndex].startTime <= playedTime)
                {
                    OnNewFrame(vertexColor);
                    transform.LookAt(Camera.main.transform);
                    transform.localScale = new Vector3(source.origScale.x * source.frames[playedIndex-1].scale.x, source.origScale.y * source.frames[playedIndex-1].scale.y, source.origScale.z * source.frames[playedIndex-1].scale.z);
                }
                else
                {
                    OnLastFrame(timeRatio2, vertexColor);
                    transform.LookAt(Camera.main.transform);
                    transform.localScale = Vector3.Lerp(new Vector3(source.origScale.x * source.frames[playedIndex - 1].scale.x, source.origScale.y * source.frames[playedIndex - 1].scale.y, source.origScale.z * source.frames[playedIndex - 1].scale.z), new Vector3(source.origScale.x * source.frames[playedIndex].scale.x, source.origScale.y * source.frames[playedIndex].scale.y, source.origScale.z * source.frames[playedIndex].scale.z), timeRatio2);
                }
                //if (source.frames[playedIndex].startTime <= playedTime)
                //{
                //    if (RotateFollow != null)
                //        transform.rotation = RotateFollow.rotation * source.frames[playedIndex].quat;
                //    else
                //        transform.rotation = source.frames[playedIndex].quat;

                //    if (source.localSpace == 1)
                //    {
                //        if (RotateFollow != null)
                //        {
                //            Vector3 targetPos = RotateFollow.TransformPoint(source.frames[playedIndex].pos) - RotateFollow.position;
                //            if (PositionFollow != null)
                //                transform.position = PositionFollow.position + targetPos;
                //            else
                //                transform.position = targetPos;
                //        }
                //        else
                //            Debug.LogError("缺少惯性坐标系骨骼");
                //    }
                //    else
                //    {
                //        if (PositionFollow != null)
                //            transform.position = PositionFollow.position + source.frames[playedIndex].pos;
                //        else
                //            transform.position = source.frames[playedIndex].pos;
                //    }

                //    transform.localScale = new Vector3(source.origScale.x * source.frames[playedIndex].scale.x, source.origScale.y * source.frames[playedIndex].scale.y, source.origScale.z * source.frames[playedIndex].scale.z);
                //    //transform.LookAt(Camera.main.transform);
                //    mRender.material.SetFloat("_Intensity", source.frames[playedIndex].TailFlags[9]);
                //    mRender.material.SetColor(vertexColor, new Color(source.frames[playedIndex].colorRGB.r, source.frames[playedIndex].colorRGB.g, source.frames[playedIndex].colorRGB.b, source.frames[playedIndex].colorRGB.a));

                //    playedIndex++;
                //    //playedTime = 0.0f;
                //}
                //else
                //{
                //    if (RotateFollow != null)
                //        transform.rotation = RotateFollow.rotation * source.frames[playedIndex].quat;
                //    else
                //        transform.rotation = source.frames[playedIndex].quat;

                //    if (source.localSpace == 1)
                //    {
                //        if (PositionFollow != null)
                //        {
                //            Vector3 targetPos = RotateFollow.TransformPoint(Vector3.Lerp(source.frames[playedIndex - 1].pos, source.frames[playedIndex].pos, timeRatio2)) - RotateFollow.position;
                //            if (PositionFollow != null)
                //                transform.position = PositionFollow.position + targetPos;
                //            else
                //                transform.position = targetPos;
                //        }
                //        else
                //            Debug.LogError("缺少惯性坐标骨骼");
                //    }
                //    else
                //    {
                //        if (PositionFollow != null)
                //            transform.position = PositionFollow.position + Vector3.Lerp(source.frames[playedIndex - 1].pos, source.frames[playedIndex].pos, timeRatio2);
                //        else
                //            transform.position = Vector3.Lerp(source.frames[playedIndex - 1].pos, source.frames[playedIndex].pos, timeRatio2);
                //    }

                //    transform.localScale = Vector3.Lerp(new Vector3(source.origScale.x * source.frames[playedIndex - 1].scale.x, source.origScale.y * source.frames[playedIndex - 1].scale.y, source.origScale.z * source.frames[playedIndex - 1].scale.z), new Vector3(source.origScale.x * source.frames[playedIndex].scale.x, source.origScale.y * source.frames[playedIndex].scale.y, source.origScale.z * source.frames[playedIndex].scale.z), timeRatio2);
                //    //transform.LookAt(Camera.main.transform);
                //    mRender.material.SetFloat("_Intensity", Mathf.Lerp(source.frames[playedIndex - 1].TailFlags[9], source.frames[playedIndex].TailFlags[9], timeRatio2));
                //    mRender.material.SetColor(vertexColor, Color.Lerp(new Color(source.frames[playedIndex - 1].colorRGB.r, source.frames[playedIndex - 1].colorRGB.g, source.frames[playedIndex - 1].colorRGB.b, source.frames[playedIndex - 1].colorRGB.a), new Color(source.frames[playedIndex].colorRGB.r, source.frames[playedIndex].colorRGB.g, source.frames[playedIndex].colorRGB.b, source.frames[playedIndex].colorRGB.a), timeRatio2));

                //}
            }
            else if (source.EffectType == "MODEL")
            {
                //第一帧开始时间为-1.表示无限时播放
                if (source.frames[playedIndex].startTime <= playedTime)
                {
                    OnNewFrame(vertexColor);
                    transform.localScale = source.frames[playedIndex - 1].scale;
                }
                else
                {
                    OnLastFrame(timeRatio2, vertexColor);
                    transform.localScale = Vector3.Slerp(source.frames[playedIndex - 1].scale, source.frames[playedIndex].scale, timeRatio2);
                }
            }
            else if (source.EffectType == "PARTICLE")
            {
                if (source.frames[playedIndex].startTime <= playedTime)
                {
                    OnNewFrame(vertexColor);
                    //transform.localScale = source.frames[playedIndex - 1].scale;
                    ParticleSystemRenderer render = particle.GetComponent<ParticleSystemRenderer>();
                    render.material.SetColor("_Color", source.frames[playedIndex - 1].colorRGB);
                    render.material.SetColor(vertexColor, source.frames[playedIndex - 1].colorRGB);
                    //粒子不要设置强度了，原版本的粒子实现和UNITY的不一样render.material.SetFloat("_Intensity", source.frames[playedIndex].TailFlags[9]);
                    particle.Emit(1);
                    //particle.Simulate();
                    playedIndex++;
                    //playedTime = 0.0f;
                }
                else
                {
                    OnLastFrame(timeRatio2, vertexColor);
                    //transform.localScale = Vector3.Lerp(source.frames[playedIndex - 1].scale, source.frames[playedIndex].scale, timeRatio2);
                    //ParticleSystemRenderer render = particle.GetComponent<ParticleSystemRenderer>();
                    //render.material.SetColor("_Color", new Color(source.frames[playedIndex].colorRGB.r, source.frames[playedIndex].colorRGB.g, source.frames[playedIndex].colorRGB.b, source.frames[playedIndex].colorRGB.a));
                    //render.material.SetColor(vertexColor, new Color(source.frames[playedIndex].colorRGB.r, source.frames[playedIndex].colorRGB.g, source.frames[playedIndex].colorRGB.b, source.frames[playedIndex].colorRGB.a));
                    //particle.Emit(1);
                    //render.material.SetFloat("_Intensity", source.frames[playedIndex].TailFlags[9]);
                }
            }
            else if (source.EffectType == "DONUT")
            {
                if (source.frames[playedIndex].startTime <= playedTime)
                {
                    OnNewFrame(vertexColor);
                    transform.localScale = source.frames[playedIndex-1].scale;
                }
                else
                {
                    OnLastFrame(timeRatio2, vertexColor);
                    transform.localScale = Vector3.Lerp(source.frames[playedIndex - 1].scale, source.frames[playedIndex].scale, timeRatio2);
                }
                //if (source.frames[playedIndex].startTime <= playedTime)
                //{
                //    if (RotateFollow != null)
                //        transform.rotation = RotateFollow.rotation * source.frames[playedIndex].quat;
                //    else
                //        transform.rotation = source.frames[playedIndex].quat;
                //    transform.localPosition = source.frames[playedIndex].pos;
                //    transform.localScale = source.frames[playedIndex].scale;
                //    mRender.material.SetFloat("_Intensity", source.frames[playedIndex].TailFlags[9]);
                //    mRender.material.SetColor(vertexColor, source.frames[playedIndex].colorRGB);
                //    playedIndex++;
                //    //playedTime = 0.0f;
                //}
                //else
                //{
                //    if (RotateFollow != null)
                //        transform.rotation = RotateFollow.rotation * source.frames[playedIndex].quat;
                //    else
                //        transform.rotation = Quaternion.Slerp(source.frames[playedIndex - 1].quat, source.frames[playedIndex].quat, timeRatio2);
                //    transform.localPosition = Vector3.Lerp(source.frames[playedIndex - 1].pos, source.frames[playedIndex].pos, timeRatio2);
                //    transform.localScale = Vector3.Lerp(source.frames[playedIndex - 1].scale, source.frames[playedIndex].scale, timeRatio2);
                //    mRender.material.SetFloat("_Intensity", Mathf.Lerp(source.frames[playedIndex - 1].TailFlags[9], source.frames[playedIndex].TailFlags[9], timeRatio2));
                //    mRender.material.SetColor(vertexColor, Color.Lerp(source.frames[playedIndex - 1].colorRGB, source.frames[playedIndex].colorRGB, timeRatio2));
                //}
            }
            else if (source.EffectType == "DRAG")
            {
                if (source.frames[playedIndex].startTime <= playedTime)
                {
                    OnNewFrame(vertexColor);
                    transform.localScale = source.frames[playedIndex-1].scale;
                }
                else
                {
                    OnLastFrame(timeRatio2, vertexColor);
                    transform.localScale = Vector3.Lerp(source.frames[playedIndex - 1].scale, source.frames[playedIndex].scale, timeRatio2);
                }
                //if (source.frames[playedIndex].startTime <= playedTime)
                //{
                //    if (RotateFollow != null)
                //        transform.rotation = RotateFollow.rotation * source.frames[playedIndex].quat;
                //    else
                //        transform.rotation = source.frames[playedIndex].quat;
                //    transform.localPosition = source.frames[playedIndex].pos;
                //    transform.localScale = source.frames[playedIndex].scale;
                //    mRender.material.SetFloat("_Intensity", source.frames[playedIndex].TailFlags[9]);
                //    mRender.material.SetColor(vertexColor, source.frames[playedIndex].colorRGB);
                //    playedIndex++;
                //    //playedTime = 0.0f;
                //}
                //else
                //{
                //    if (RotateFollow != null)
                //        transform.rotation = RotateFollow.rotation * source.frames[playedIndex].quat;
                //    else
                //        RotateFollow.transform.rotation = Quaternion.Slerp(source.frames[playedIndex - 1].quat, source.frames[playedIndex].quat, timeRatio2);
                //    transform.localPosition = Vector3.Lerp(source.frames[playedIndex - 1].pos, source.frames[playedIndex].pos, timeRatio2);
                //    transform.localScale = Vector3.Lerp(source.frames[playedIndex - 1].scale, source.frames[playedIndex].scale, timeRatio2);
                //    mRender.material.SetFloat("_Intensity", Mathf.Lerp(source.frames[playedIndex - 1].TailFlags[9], source.frames[playedIndex].TailFlags[9], timeRatio2));
                //    mRender.material.SetColor(vertexColor, Color.Lerp(source.frames[playedIndex - 1].colorRGB, source.frames[playedIndex].colorRGB, timeRatio2));
                //}
            }

            yield return 0;
        }
        //?是循环还是删除
        //playedIndex = 0;
        //yield return 0;
        PlayDone = true;
        if (parentSfx != null)
            parentSfx.OnPlayDone(this);
        else
            Destroy(gameObject);
        yield break;
    }

    public void ShowKeyFrame(string vertexColor)
    {
        if (RotateFollow != null)
            transform.rotation = RotateFollow.rotation * source.frames[playedIndex].quat;
        else
            transform.rotation = source.frames[playedIndex].quat;

        //if (source.localSpace == 1)
        //{
        if (RotateFollow != null)
        {
            Vector3 targetPos = RotateFollow.TransformPoint(source.frames[playedIndex].pos) - RotateFollow.position;
            if (PositionFollow != null)
                transform.position = PositionFollow.position + targetPos;
            else
                transform.position = targetPos;
        }
        //}
        else
        {
            if (PositionFollow != null)
                transform.position = PositionFollow.position + source.frames[playedIndex].pos;
            else
                transform.position = source.frames[playedIndex].pos;
        }

        mRender.material.SetFloat("_Intensity", source.frames[playedIndex].TailFlags[9]);
        mRender.material.SetColor(vertexColor, source.frames[playedIndex].colorRGB);
    }

    public void OnNewFrame(string vertexColor)
    {
        ShowKeyFrame(vertexColor);
        playedIndex++;
    }

    public void OnLastFrame(float timeRatio2, string vertexColor)
    {
        if (RotateFollow != null)
            transform.rotation = RotateFollow.rotation * Quaternion.Slerp(source.frames[playedIndex - 1].quat, source.frames[playedIndex].quat, timeRatio2);
        else
            transform.rotation = Quaternion.Slerp(source.frames[playedIndex - 1].quat, source.frames[playedIndex].quat, timeRatio2);

        //if (source.localSpace == 1)
        //{
            if (RotateFollow != null)
            {
                Vector3 targetPos = RotateFollow.TransformPoint(Vector3.Lerp(source.frames[playedIndex - 1].pos, source.frames[playedIndex].pos, timeRatio2)) - RotateFollow.position;
                if (PositionFollow != null)
                    transform.position = PositionFollow.position + targetPos;
                else
                    transform.position = targetPos;
            }
        //}
        else
        {
            if (PositionFollow != null)
                transform.position = PositionFollow.position + Vector3.Lerp(source.frames[playedIndex - 1].pos, source.frames[playedIndex].pos, timeRatio2);
            else
                transform.position = Vector3.Lerp(source.frames[playedIndex - 1].pos, source.frames[playedIndex].pos, timeRatio2);
        }

        
        mRender.material.SetFloat("_Intensity", Mathf.Lerp(source.frames[playedIndex - 1].TailFlags[9], source.frames[playedIndex].TailFlags[9], timeRatio2));
        mRender.material.SetColor(vertexColor, Color.Lerp(source.frames[playedIndex - 1].colorRGB, source.frames[playedIndex].colorRGB, timeRatio2));
    }

    bool damageEnabled;
    //这个只碰撞瓶罐和建筑。与角色的碰撞由其他地方处理.
    public void ChangeAttack(bool open)
    {
        //播放完毕不允许再开开，否则特效重复攻击
        if (PlayDone && open)
            return;
        damageEnabled = open;
        
    }

    void ChangeAttackCore()
    {
        if (damageBox != null && damageBox.enabled != damageEnabled)
        {
            damageBox.enabled = damageEnabled;
            damageBox.isTrigger = damageEnabled;
        }
    }
}
