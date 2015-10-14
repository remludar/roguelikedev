using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using System.IO;

namespace NeoKabutoTutorial2
{
    class Game : GameWindow
    {
        int shaderProgramID;
        int vsID, fsID;
        int vertexColorAttribute, vertexPositionAttribute;
        int modelViewUniform;

        int vboPosition, vboColor, vboModelView;
        
        Vector3[] vertData, colorData;
        Matrix4[] modelViewData;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            initProgram();

            vertData = new Vector3[]{
                new Vector3(-0.8f, -0.8f, +0.0f),
                new Vector3(+0.8f, -0.8f, +0.0f),
                new Vector3(+0.0f, +0.8f, +0.0f)
            };

            colorData = new Vector3[]{
                new Vector3(+1.0f, +0.0f, +0.0f),
                new Vector3(+0.0f, +0.0f, +1.0f),
                new Vector3(+0.0f, +1.0f, +0.0f)
            };

            modelViewData = new Matrix4[]{
                Matrix4.Identity
            };

            Title = "Hello OpenTK!";
            GL.ClearColor(Color.CornflowerBlue);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            GL.BindBuffer(BufferTarget.ArrayBuffer, vboPosition);
            GL.BufferData<Vector3>(BufferTarget.ArrayBuffer, (IntPtr)(vertData.Length * Vector3.SizeInBytes), vertData, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(vertexPositionAttribute, 3, VertexAttribPointerType.Float, false, 0, 0);

            GL.BindBuffer(BufferTarget.ArrayBuffer, vboColor);
            GL.BufferData<Vector3>(BufferTarget.ArrayBuffer, (IntPtr)(colorData.Length * Vector3.SizeInBytes), colorData, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(vertexColorAttribute, 3, VertexAttribPointerType.Float, false, 0, 0);

            GL.UniformMatrix4(modelViewUniform, false, ref modelViewData[0]);

            GL.UseProgram(shaderProgramID);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            GL.Viewport(0, 0, Width, Height);
            GL.Enable(EnableCap.DepthTest);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.EnableVertexAttribArray(vertexPositionAttribute);
            GL.EnableVertexAttribArray(vertexColorAttribute);

            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);

            GL.DisableVertexAttribArray(vertexPositionAttribute);
            GL.DisableVertexAttribArray(vertexColorAttribute);
            
            GL.Flush();
            SwapBuffers();
        }

        void initProgram()
        {
            shaderProgramID = GL.CreateProgram();
            loadShader("vs.glsl", ShaderType.VertexShader, shaderProgramID, out vsID);
            loadShader("fs.glsl", ShaderType.FragmentShader, shaderProgramID, out fsID);
            GL.LinkProgram(shaderProgramID);
            Console.WriteLine(GL.GetProgramInfoLog(shaderProgramID));

            vertexPositionAttribute = GL.GetAttribLocation(shaderProgramID, "vPosition");
            vertexColorAttribute = GL.GetAttribLocation(shaderProgramID, "vColor");
            modelViewUniform = GL.GetUniformLocation(shaderProgramID, "modelView");

            if (vertexPositionAttribute == -1 || vertexColorAttribute == -1 || modelViewUniform == -1)
            {
                Console.WriteLine("Error binding attributes");
            }

            GL.GenBuffers(1, out vboPosition);
            GL.GenBuffers(1, out vboColor);
            GL.GenBuffers(1, out vboModelView);
        }

        void loadShader(string filename, ShaderType type, int program, out int address)
        {
            address = GL.CreateShader(type);
            using (StreamReader reader = new StreamReader(filename))
            {
                GL.ShaderSource(address, reader.ReadToEnd());
            }
            GL.CompileShader(address);
            GL.AttachShader(program, address);
            Console.WriteLine(GL.GetShaderInfoLog(address));
        }
    }
}
