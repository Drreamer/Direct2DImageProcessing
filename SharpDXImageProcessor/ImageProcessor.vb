Imports SharpDX
Imports SharpDX.IO
Imports d2 = SharpDX.Direct2D1
Imports d3d = SharpDX.Direct3D11
Imports dxgi = SharpDX.DXGI
Imports wic = SharpDX.WIC
Imports dw = SharpDX.DirectWrite
Imports System.IO
Imports System.Drawing
Imports SharpDX.Direct2D1
Imports System.Runtime.InteropServices

Public Class ImageProcessor
   Implements IDisposable

   Private defaultDevice As SharpDX.Direct3D11.Device
   Private d3dDevice As d3d.Device1
   Private dxgiDevice As dxgi.Device
   Private d2dDevice As d2.Device
   Private d2dContext As d2.DeviceContext
   Private d2PixelFormat As d2.PixelFormat
   Private decoder As wic.PngBitmapDecoder
   Private d2SourceBitmap As d2.Bitmap1

   Public Sub New()

      defaultDevice = New SharpDX.Direct3D11.Device(SharpDX.Direct3D.DriverType.Hardware, d3d.DeviceCreationFlags.BgraSupport Or d3d.DeviceCreationFlags.None Or d3d.DeviceCreationFlags.Debug)
      d3dDevice = defaultDevice.QueryInterface(Of d3d.Device1)()
      dxgiDevice = d3dDevice.QueryInterface(Of dxgi.Device)()
      d2dDevice = New d2.Device(dxgiDevice)
      d2dContext = New d2.DeviceContext(d2dDevice, d2.DeviceContextOptions.None)
      d2PixelFormat = New d2.PixelFormat(dxgi.Format.R8G8B8A8_UNorm, d2.AlphaMode.Premultiplied)
      d2SourceBitmap = New Bitmap1(d2dContext, New Size2(6000, 4000), New BitmapProperties1(d2PixelFormat))
   End Sub

    Public Sub ScaleImage(pBaseImage As System.Drawing.Bitmap, pTargetImage As System.Drawing.Bitmap)
       FillSourceBitmap(pBaseImage)
       Dim cropEffect = New d2.Effects.Crop(d2dContext)
       cropEffect.SetInput(0, d2SourceBitmap, True)
       cropEffect.Rectangle = New Mathematics.Interop.RawVector4(0, 0, pBaseImage.Width, pBaseImage.Height)
       Dim scaleEffect = New d2.Effects.Scale(d2dContext)
       scaleEffect.SetInput(0, cropEffect.Output, True)
       scaleEffect.ScaleAmount = New SharpDX.Mathematics.Interop.RawVector2(pTargetImage.Width / pBaseImage.Width, pTargetImage.Height / pBaseImage.Height)
 
       Dim d2TargetBitmapProps = New d2.BitmapProperties1(d2PixelFormat, 96, 96, d2.BitmapOptions.Target)
       Dim d2TargetBitmap = New d2.Bitmap1(d2dContext, New Size2(pTargetImage.Width, pTargetImage.Height), d2TargetBitmapProps)
       d2dContext.Target = d2TargetBitmap
       d2dContext.BeginDraw()
       d2dContext.DrawImage(scaleEffect,)
       d2dContext.EndDraw()
 
       CopyBitmapFromDxToGDI(d2TargetBitmap, pTargetImage)
    End Sub
 
    Private Sub FillSourceBitmap(bmp As Drawing.Bitmap)
       Dim bmpData As Imaging.BitmapData = bmp.LockBits(New Drawing.Rectangle(0, 0, bmp.Width, bmp.Height), Imaging.ImageLockMode.ReadWrite, Imaging.PixelFormat.Format32bppPArgb)
       d2SourceBitmap.CopyFromMemory(bmpData.Scan0, bmpData.Stride,
                               New Mathematics.Interop.RawRectangle(0, 0, bmpData.Width, bmpData.Height))
       bmp.UnlockBits(bmpData)
    End Sub
 
    Private Sub CopyBitmapFromDxToGDI(bitmap As d2.Bitmap1, bmp As Drawing.Bitmap)
       Dim d2TempBitmapProps = New d2.BitmapProperties1(d2PixelFormat, 96, 96, BitmapOptions.CpuRead Or BitmapOptions.CannotDraw)
       Dim d2TempBitmap = New d2.Bitmap1(d2dContext, New Size2(bmp.Width, bmp.Height), d2TempBitmapProps)      
       d2TempBitmap.CopyFromRenderTarget(d2dContext)
       
       Dim surface = d2TempBitmap.Surface
       Dim dataStream As DataStream = Nothing
       Dim dataRectangle As DataRectangle = surface.Map(dxgi.MapFlags.Read, dataStream)
       Dim bmpData As Imaging.BitmapData = bmp.LockBits(New Drawing.Rectangle(0, 0, bmp.Width, bmp.Height), Imaging.ImageLockMode.ReadWrite, Imaging.PixelFormat.Format32bppPArgb)
 
       Dim offset = bmpData.Reserved
       Dim buffer(4) As Byte
       For y As Integer = 0 To surface.Description.Height - 1
          For x As Integer = 0 To surface.Description.Width - 1
             'from R8G8B8A8 to Argb
             dataStream.Seek((y * dataRectangle.Pitch) + (x * 4), SeekOrigin.Begin)
             dataStream.Read(buffer, 0, 4)           
             
             Marshal.WriteByte(bmpData.Scan0, offset, buffer(3))            
             Marshal.WriteByte(bmpData.Scan0, offset+1, buffer(0))
             Marshal.WriteByte(bmpData.Scan0, offset+2, buffer(1))
             Marshal.WriteByte(bmpData.Scan0, offset+3, buffer(2))
             offset += 4
          Next
       Next
       bmp.UnlockBits(bmpData)
       surface.Unmap()
       dataStream.Dispose()
       d2TempBitmap.Dispose()
    End Sub

   Public Sub Dispose() Implements IDisposable.Dispose
      d2SourceBitmap.Dispose()
      decoder.Dispose()
      d2dContext.Dispose()
      d2dDevice.Dispose()
      dxgiDevice.Dispose()
      d3dDevice.Dispose()
      defaultDevice.Dispose()
   End Sub
End Class
