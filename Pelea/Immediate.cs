﻿using System;
using System.IO;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace Immediate
{
    class InteractiveWindow : GameWindow
    {
        private Vector3[] vertices = new Vector3[3];
        private float[] color = { 0.0f, 0.0f, 0.0f, 1.0f };
        private float cameraAngleX = 0.0f;
        private float cameraAngleY = 0.0f;

        

        public InteractiveWindow()
            : base(800, 600, GraphicsMode.Default, "Triangle Color and Camera Control")
        {
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            GL.ClearColor(0.66f, 0.25f, 0.95f, 1.0f);

            LoadTriangleVertices("triangle_vertices.txt");

            GL.Enable(EnableCap.DepthTest);

            Console.WriteLine("Controale: \n R - amplifica culoarea rosie \n G - amplifica culoarea verde \n B - amplifica culoarea albastra \n C - reseteaza culoarea triunghiului \n");

        }

        private void LoadTriangleVertices(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);
            for (int i = 0; i < 3; i++)
            {
                string[] parts = lines[i].Split(' ');
                vertices[i] = new Vector3(float.Parse(parts[0]), float.Parse(parts[1]), float.Parse(parts[2]));
            }
        }

        bool clear;
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);


            if (Keyboard.GetState().IsKeyDown(Key.R)) color[0] = Math.Min(1.0f, color[0] + 0.01f);
            if (Keyboard.GetState().IsKeyDown(Key.G)) color[1] = Math.Min(1.0f, color[1] + 0.01f);
            if (Keyboard.GetState().IsKeyDown(Key.B)) color[2] = Math.Min(1.0f, color[2] + 0.01f);


            if (Keyboard.GetState().IsKeyDown(Key.C) && !clear)
            {
                Console.Clear();
                Console.WriteLine("Controale: \n R - amplifica culoarea rosie \n G - amplifica culoarea verde \n B - amplifica culoarea albastra \n C - reseteaza culoarea triunghiului \n");

                clear = true;

                Console.WriteLine($"Vertex 1 RGB: " + color[0].ToString() + ", 0.0, 0.0");
                Console.WriteLine($"Vertex 2 RGB: 0.0, " + color[1].ToString() + ", 0.0");
                Console.WriteLine($"Vertex 3 RGB: 0.0, 0.0, " + color[2].ToString());
                color[0] = 0.0f; color[1] = 0.0f; color[2] = 0.0f;
            }

            if (Keyboard.GetState().IsKeyUp(Key.C))
            {
                clear = false;
            }

        }

        protected override void OnMouseMove(MouseMoveEventArgs e)
        {
            base.OnMouseMove(e);


            cameraAngleX += e.XDelta * 0.1f;
            cameraAngleY += e.YDelta * 0.1f;
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            GL.Rotate(cameraAngleX, 0.0f, 1.0f, 0.0f);
            GL.Rotate(cameraAngleY, 1.0f, 0.0f, 0.0f);

            //desenare triunghi
            GL.Begin(PrimitiveType.Triangles);

            GL.Color3(color[0], 0.0f, 0.0f);
            GL.Vertex3(vertices[0]);


            GL.Color3(0.0f, color[1], 0.0f);
            GL.Vertex3(vertices[1]);


            GL.Color3(0.0f, 0.0f, color[2]);
            GL.Vertex3(vertices[2]);


            GL.End();

            SwapBuffers();
        }


        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, Width, Height);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(-1.0, 1.0, -1.0, 1.0, -10.0, 10.0);
            GL.MatrixMode(MatrixMode.Modelview);
        }

        protected override void OnKeyDown(KeyboardKeyEventArgs e)
        {
            base.OnKeyDown(e);

            // Închidem aplicația la apăsarea tastei Escape
            if (e.Key == Key.Escape)
                this.Close();
        }

        
    }
}
