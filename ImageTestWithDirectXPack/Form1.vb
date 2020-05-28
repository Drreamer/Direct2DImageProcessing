Imports Microsoft.Graphics.Canvas
Imports SharpDXImageProcessor

Public Class Form1
   Private mvImages As Bitmap()
   Private mvImageProcessor As ImageProcessor
   Public Sub New()
      InitializeComponent()

      Try
         Dim files = System.IO.Directory.GetFiles(System.IO.Path.Combine(Application.StartupPath, "Images"), "*.jpg")
         mvImages = files.Select(Function(fn)
                                    Dim orig = TryCast(Bitmap.FromFile(fn), Bitmap)
                                    Dim clone = New Bitmap(orig.Width, orig.Height, System.Drawing.Imaging.PixelFormat.Format32bppPArgb)
                                    Using gr = Graphics.FromImage(clone)
                                       gr.DrawImage(orig, New Rectangle(0, 0, clone.Width, clone.Height))
                                    End Using
                                    Return clone
                                 End Function).ToArray()
         mvImageProcessor = New ImageProcessor()
      Catch ex As Exception

      End Try
   End Sub

   Private Sub bUpdate_Click(sender As Object, e As EventArgs) Handles bUpdate.Click
      pImages.Invalidate()
   End Sub

   Private columnCount = 5, rowCount = 3

   Private Sub pImages_Paint(sender As Object, e As PaintEventArgs) Handles pImages.Paint
      If mvImages Is Nothing OrElse mvImages.Length = 0 Then
         e.Graphics.DrawString("No images loaded. Add target '*.jpg' files to the Images folder.", ListBox1.Font, Brushes.Red, New PointF(10, 10))
      Else
         Dim sw = Stopwatch.StartNew()
         Dim imageWidth As Integer = CInt(pImages.Width / columnCount)
         Dim imageHeight As Integer = CInt(pImages.Height / rowCount)
         For col = 0 To columnCount - 1
            For row = 0 To rowCount - 1
               Dim imageIndex = (row * 4 + col) Mod mvImages.Length
               
               'RenderUsingGDI(e.Graphics, mvImages(imageIndex), New Rectangle(imageWidth * col, imageHeight * row, imageWidth, imageHeight))

               RenderUsingDirectX(e.Graphics, mvImages(imageIndex), New Rectangle(imageWidth * col, imageHeight * row, imageWidth, imageHeight))

            Next
         Next
         sw.Stop()
         ListBox1.Items.Insert(0, $"Rendered in {sw.ElapsedMilliseconds}ms.")
      End If
   End Sub

   Private Sub RenderUsingDirectX(graphics As Graphics, bitmap As Bitmap, bounds As Rectangle)
      Try
         Dim targetBitmap = New Bitmap(bounds.Width, bounds.Height, Imaging.PixelFormat.Format32bppPArgb)
         mvImageProcessor.ScaleImage(bitmap, targetBitmap)
         graphics.DrawImage(targetBitmap, bounds.Location)
      Catch ex As Exception

      End Try

   End Sub

   Private Sub pImages_Resize(sender As Object, e As EventArgs) Handles pImages.Resize
      Me.Text = pImages.Size.ToString()
      pImages.Invalidate()
   End Sub

   Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
      Me.Width += 1
   End Sub

   Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
      Me.Width -= 1
   End Sub

   Private Sub RenderUsingGDI(graphics As Graphics, bitmap As Image, bounds As Rectangle)
      graphics.DrawImage(bitmap, bounds)
   End Sub
End Class
