using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace AutoClicker;

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

    [DllImport("user32.dll", EntryPoint = "GetCursorPos")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool GetCursorPosition(out MousePoint mousePoint);

    public static MousePoint GetCursorPosition()
    {
        if (!GetCursorPosition(out var currentMousePoint))
        {
            currentMousePoint = new();
        }

        return currentMousePoint;
    }

    [DllImport("user32.dll", EntryPoint = "mouse_event")]
    private static extern void MouseEvent(int dwFlags, int dx, int dy, int dwData = 0, int dwExtraInfo = 0);

    public static void MouseEvent(MouseEventFlags value)
    {
        var position = GetCursorPosition();
        MouseEvent((int)value, position.X, position.Y);
    }

    [StructLayout(LayoutKind.Sequential)]
    public readonly record struct MousePoint(int X, int Y);
}