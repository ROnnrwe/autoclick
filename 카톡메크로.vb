Imports System.Runtime.InteropServices
Imports System.Threading

Public Class Form1
    <DllImport("user32.dll", SetLastError:=True)>
    Private Shared Function SendInput(
        ByVal nInputs As UInteger,
        ByRef pInputs As INPUT,
        ByVal cbSize As Integer
    ) As UInteger
    End Function

    <StructLayout(LayoutKind.Sequential)>
    Structure INPUT
        Public type As Integer
        Public mi As MOUSEINPUT ' MouseInput 구조체 추가
    End Structure

    <StructLayout(LayoutKind.Sequential)>
    Structure MOUSEINPUT
        Public dx As Integer
        Public dy As Integer
        Public mouseData As UInteger
        Public dwFlags As UInteger
        Public time As UInteger
        Public dwExtraInfo As IntPtr
    End Structure

    Const MOUSEEVENTF_LEFTDOWN As UInteger = &H2
    Const MOUSEEVENTF_LEFTUP As UInteger = &H4

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' 버튼 클릭 시 동작
        Dim timer As New Timer(AddressOf StartClicking, Nothing, 10000, Timeout.Infinite)
    End Sub

    Private Sub StartClicking(state As Object)
        Dim clickCount As Integer = 2000 * 20 ' 초당 100번 * 20초
        Dim clickInterval As Integer = 10 ' 밀리초 (0.01초)

        Dim cursorPosition As Point = Cursor.Position

        For i As Integer = 1 To clickCount
            SimulateMouseClick(cursorPosition)
            Thread.Sleep(clickInterval)
        Next
    End Sub

    Private Sub SimulateMouseClick(cursorPosition As Point)
        Dim input As New INPUT()
        input.type = 0 ' INPUT_MOUSE

        ' 마우스 클릭
        input.mi.dx = cursorPosition.X
        input.mi.dy = cursorPosition.Y
        input.mi.dwFlags = MOUSEEVENTF_LEFTDOWN
        SendInput(1, input, Marshal.SizeOf(input))

        input.mi.dwFlags = MOUSEEVENTF_LEFTUP
        SendInput(1, input, Marshal.SizeOf(input))
    End Sub
End Class
