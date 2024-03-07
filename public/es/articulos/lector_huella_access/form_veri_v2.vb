Option Compare Database

Dim WithEvents Capture As dpfpCapture
Dim CreateFtrs As DPFPFeatureExtraction
Dim Verify As DPFPVerification
Dim ConvertSample As DPFPSampleConversion

Private Sub ReportStatus(ByVal Str As String)
 ' Add string to list box.
 status.AddItem (Str)
 ' Move list box selection down.
End Sub

Private Sub Capture_OnReaderConnect(ByVal ReaderSerNum As String)
 ReportStatus ("The fingerprint reader was connected.")
End Sub

Private Sub Capture_OnReaderDisconnect(ByVal ReaderSerNum As String)
 ReportStatus ("The fingerprint reader was disconnected.")
End Sub

Private Sub Capture_OnFingerTouch(ByVal ReaderSerNum As String)
 ReportStatus ("The fingerprint reader was touched.")
End Sub

Private Sub Capture_OnFingerGone(ByVal ReaderSerNum As String)
 ReportStatus ("The finger was removed from the fingerprint reader.")
End Sub

Private Sub Capture_OnSampleQuality(ByVal ReaderSerNum As String, ByVal Feadback As DPFPCaptureFeedbackEnum)
 If Feadback = CaptureFeedbackGood Then
  ReportStatus ("The quality of fingerprint sample is good.")
  Else
  ReportStatus ("The quality of fingerprint sample is poor.")
  End If
End Sub

Private Sub Capture_OnComplete(ByVal ReaderSerNum As String, ByVal Sample As Object)
 Dim Feadback As DPFPCaptureFeedbackEnum
 Dim Res As DPFPVerificationResult
 Dim Templ As DPFPTemplate
 ReportStatus ("The fingerprint was captured.")
 ' Draw fingerprint image.
 'DrawPicture ConvertSample.ConvertToPicture(Sample)
 ' Process sample and create feature set for purpose of verification.
 Feedback = CreateFtrs.CreateFeatureSet(Sample, DataPurposeVerification)
 ' Quality of sample is not good enough to produce feature set.
 If Feadback = CaptureFeedbackGood Then
  Capture.StopCapture
  Prompt.Caption = "Touch the fingerprint reader with a different finger."
  'Set Templ = GetTemplate()
  If Templ Is Nothing Then
    ' Declare variables
    Dim db As DAO.Database
    Dim rs As DAO.Recordset
    Dim sqlQuery As String

    ' Set the current database
    Set db = CurrentDb

    ' Define your SQL query
    sqlQuery = "SELECT * FROM Empleados"

    ' Open a recordset based on the SQL query
    Set rs = db.OpenRecordset(sqlQuery)

    ' Loop through the recordset and display data (replace with your logic)
    Do While Not rs.EOF
        ' Add more fields as needed
        Dim byteData() As Byte
        byteData = rs!huella
        'Dim fieldType As Integer
        'fieldType = VarType(rs!fptemplate)
        Set Templ = New DPFPTemplate
        Templ.Deserialize byteData
    
        ' Display the type
        'MsgBox "Type of fptemplate field: " & fieldType
        
        ' Deserialize the template from the database field
        'Templ.Deserialize var
        
        ' Compare feature set with template.
        Set Res = Verify.Verify(CreateFtrs.FeatureSet, Templ)
        
        ' Show results of comparison.
        far.Caption = Res.FARAchieved
        If Res.Verified = True Then
            MsgBox "Nombre: " & rs!nombre_completo
            ReportStatus ("The fingerprint was verified.")
            Exit Do
        Else
            ReportStatus ("The fingerprint was not verified.")
        End If
        rs.MoveNext
    Loop

    ' Close the recordset
    rs.Close

    ' Clean up
    Set rs = Nothing
    Set db = Nothing
  Else
    MsgBox "Nothing here"
  End If
 End If
 End Sub

Private Sub close_Click()
    Capture.StopCapture
    DoCmd.Close

End Sub

Private Sub Form_Current()

On Error GoTo ErrHere
 
 ' Create capture operation.
 Set Capture = New dpfpCapture
 ' Start capture operation.
 Capture.StartCapture
 ' Create DPFPFeatureExtraction object.
 Set CreateFtrs = New DPFPFeatureExtraction
 ' Create DPFPVerification object.
 Set Verify = New DPFPVerification
 ' Create DPFPSampleConversion object.
 Set ConvertSample = New DPFPSampleConversion


Exithere:
    Exit Sub
ErrHere:
    MsgBox Err.Description
    Resume Exithere
    
End Sub


