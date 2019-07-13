Shader "Unlit/UnlitShaderOutline"
{
    Properties
    {
		_Color("Main Color", Color) = (231, 142, 0.5, 1)
        _MainTex ("Texture", 2D) = "white" {}
		_OutlineColor("Outline color", Color) = (0,0,0,1)
		_OutlineWidth("Outline width", Range(0.01, 5.0)) = 0.04
    }
	
	/*
	CGINCLUDE
	#include "UnityCG.cginc"

	struct appdata
	{
			float4 vertex : POSITION;
			float3 normal : NORMAL;
	};

	struct v2f
	{
		float4 pos : POSITION;
		//float4 color : COLOR;
		float3 normal : NORMAL;
	};

	float _OutlineWidth;
	float4 _OutlineColor;

	ENDCG
	*/

	/*//vertex shader
	v2f vert(appdata v)
	{
		v.position.xyz = v.position.xyz + v.normal* _OutLineWidth;
		v2f o;
		o.pos = UnityObjectToClipPos(v.vertex);
		//o.color = _OutlineColor;
		return o; 
	}
	*/

    SubShader
    {
        //Tags { "RenderType"="Opaque" }
		Tags { "Queue" = "Transparent" }
        LOD 100

        Pass
        {
			ZWrite Off // write into Z buffer
			
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
				float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
				float3 normal : NORMAL;
				float4 color : COLOR;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
			float4 _OutlineColor;
			float _OutlineWidth;
			float4 _Color;

            v2f vert (appdata v)
            {
				v.vertex.xyz = v.vertex.xyz + v.normal* _OutlineWidth; //wau
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o  ;
            }

			/*//wau
			half4 frag(v2f) : COLOR
			{
				return _Color;
			}; */


            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
				//fixed4 col = _Color;
                // apply fog
                //UNITY_APPLY_FOG(i.fogCoord, col);
				i.color = _Color;
				UNITY_APPLY_FOG(i.color, col);
                return col * _OutlineColor  ;


            }
            ENDCG
        }

		Pass //normal render
		{
			ZWrite On

			Material
			{
				Diffuse[_Color]
				Ambient[_Color]
			}
			
			Lighting On

			SetTexture[_MainTex]
			{
				ConstantColor[_MainColor]
			}
			/*
			SetTexture[_MainTex]
			{
				Combine prevoius * primary DOUBLE	
			}*/
		}
    }
}
