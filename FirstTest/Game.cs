using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;
using OpenTK.Input;
using OpenTK.Graphics.OpenGL;
using System.Drawing;

namespace FirstTest
{
    class Game : GameWindow
    {
        float[] colors = {
            1.0f, 1.0f, 1.0f, 1.0f,
            1.0f, 1.0f, 1.0f, 1.0f,
            1.0f, 1.0f, 1.0f, 1.0f,
            1.0f, 1.0f, 1.0f, 1.0f,
        };

        byte[] triangles = {
            0, 1, 2,
            2, 3, 0
        };

        float[] vertices = {
            -0.25f, -0.25f,
            -0.25f, +0.25f,
            +0.25f, +0.25f,
            +0.25f, -0.25f
        };



        public Game(int width, int height)
            :base(width, height)
        {

        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Keyboard.KeyDown += HandleKeyDown;

            GL.EnableClientState(ArrayCap.VertexArray);
            GL.EnableClientState(ArrayCap.ColorArray);
        }

        // 1
        void HandleKeyDown(object sender, KeyboardKeyEventArgs e)
        {
            if (e.Key == Key.Escape) Exit();
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
        }


        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.ClearColor(Color.CornflowerBlue);

            GL.VertexPointer(2, VertexPointerType.Float, 0, vertices);
            GL.ColorPointer(4, ColorPointerType.Float, 0, colors);
            GL.DrawElements(PrimitiveType.Triangles, 6, DrawElementsType.UnsignedByte, triangles);

            this.SwapBuffers();
        }

        private void _Draw(List<float> vertices)
        {
            GL.Vertex2(-0.25f, 0.25f);
            GL.Vertex2(0.25f, 0.25f);
            GL.Vertex2(-0.25f, -0.25f);

            GL.Vertex2(0.25f, 0.25f);
            GL.Vertex2(0.25f, -0.25f);
            GL.Vertex2(-0.25f, -0.25f);
        }
    }
}
