﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;

namespace NeoKabutoTutorial
{
    class Game : GameWindow
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Title = "Hello OpenTK!";
            GL.ClearColor(Color.CornflowerBlue);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width, ClientRectangle.Height);
            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI/4, Width / (float)Height, 1.0f, 64.0f);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projection);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            
            Matrix4 modelview = Matrix4.LookAt(Vector3.Zero, Vector3.UnitZ, Vector3.UnitY);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref modelview);

            GL.Begin(PrimitiveType.Triangles);
            GL.Color3(1.0f, 0.0f, 0.0f);
            GL.Vertex3(-1.0f, -1.0f, 4.0f);
            GL.Color3(0.0f, 1.0f, 0.0f);
            GL.Vertex3(+1.0f, -1.0f, 4.0f);
            GL.Color3(0.0f, 0.0f, 1.0f);
            GL.Vertex3(+0.0f, +1.0f, 4.0f);
            GL.End();

            SwapBuffers();
        }
    }
}
