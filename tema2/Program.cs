using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
    internal class Immediate : GameWindow
    {
        private float _x = 0.0f; // Coordonata X 
        private float _y = 0.0f; // Coordonata Y

        public Immediate() : base(800, 600)
        {
            //Scriere in consola a comenzilor
            Console.WriteLine("Controale: \n Sageata dreapta: muta obiectul in dreapta \n Sageata stanga: muta obiectul in stanga \n Escape: oprire program");
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
          //  GL.ClearColor(Color.DarkGray);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            //Muta obiectul la stanga
            if (Keyboard.GetState().IsKeyDown(Key.Left))
                _x -= 0.05f;

            //Muta obiectul la dreapta
            if (Keyboard.GetState().IsKeyDown(Key.Right))
                _x += 0.05f;
        }

        protected override void OnMouseMove(MouseMoveEventArgs e)
        {
            base.OnMouseMove(e);

            // Mutare obiect dupa mouse
            _x = (e.X - Width / 2) / (Width / 2.0f);
            _y = -(e.Y - Height / 2) / (Height / 2.0f);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            // Desenare forma
            GL.Begin(PrimitiveType.Quads);
          //  GL.Color3(Color.Magenta);
            GL.Vertex2(_x - 0.04f, _y - 0.05f);
            GL.Vertex2(_x + 0.04f, _y - 0.05f);
            GL.Vertex2(_x + 0.04f, _y + 0.05f);
            GL.Vertex2(_x - 0.04f, _y + 0.05f);
            GL.End();

            SwapBuffers();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, Width, Height);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(-1.0, 1.0, -1.0, 1.0, -1.0, 1.0);
            GL.MatrixMode(MatrixMode.Modelview);
        }

        protected override void OnKeyDown(KeyboardKeyEventArgs e)
        {
            base.OnKeyDown(e);

            // Inchidere aplicatie
            if (e.Key == Key.Escape)
                this.Close();
        }

    }
}
