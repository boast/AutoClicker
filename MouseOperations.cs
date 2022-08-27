using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace AutoClicker
{
    [SuppressMessage("ReSharper", "UnusedMember.Global", Justification = "Library")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global", Justification = "Library")]
    internal static class MouseOperations
    {
        [Flags]
        public enum MouseEventFlags
        {
            Move = 0x00000001,
            LeftDown = 0x00000002,
            LeftUp = 0x00000004,
            RightDown = 0x00000008,
            RightUp = 0x00000010,
            MiddleDown = 0x00000020,
            MiddleUp = 0x00000040,
            Absolute = 0x00008000,
        }

        [DllImport("user32.dll", EntryPoint = "SetCursorPos")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetCursorPos(int x, int y);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetCursorPos(out MousePoint lpMousePoint);

        [DllImport("user32.dll")]
        private static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        public static void SetCursorPosition(int x, int y) => SetCursorPos(x, y);

        public static void SetCursorPosition(MousePoint point) => SetCursorPos(point.X, point.Y);

        public static MousePoint GetCursorPosition()
        {
            bool gotPoint = GetCursorPos(out var currentMousePoint);

            if (!gotPoint)
            {
                currentMousePoint = new(0, 0);
            }

            return currentMousePoint;
        }

        public static void MouseEvent(MouseEventFlags value)
        {
            var position = GetCursorPosition();

            mouse_event
                ((int)value,
                    position.X,
                    position.Y,
                    0,
                    0)
                ;
        }

        [StructLayout(LayoutKind.Sequential)]
        public readonly struct MousePoint
        {
            public readonly int X;
            public readonly int Y;

            public MousePoint(int x, int y)
            {
                X = x;
                Y = y;
            }
        }
    }
}