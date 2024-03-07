Option Compare Database
Dim WithEvents Capture As dpfpCapture
Dim CreateFtrs As DPFPFeatureExtraction
Dim CreateTempl As DPFPEnrollment
Dim ConvertSample As DPFPSampleConversion

Private Sub Capture_OnComplete(ByVal ReaderSerNum As String, ByVal Sample As Object)
Dim Feedback As DPFPCaptureFeedbackEnum
Dim blob() As Byte

 Feedback = CreateFtrs.CreateFeatureSet(Sample, DataPurposeEnrollment)
    If Feedback = CaptureFeedbackGood Then
     CreateTempl.AddFeatures CreateFtrs.FeatureSet
     samples.Caption = CreateTempl.FeaturesNeeded
       If CreateTempl.TemplateStatus = TemplateStatusTemplateReady Then
        Set Templ = CreateTempl.Template
        Capture.StopCapture
         
        blob = Templ.Serialize
        
        huella.Value = blob

        MsgBox "The fingerprint template was created."
       End If
    End If

Exithere:
    Exit Sub
ErrHere:
    MsgBox Err.Description
    Resume Exithere
End Sub

Private Sub agregar_Click()
  DoCmd.Close
  DoCmd.OpenForm "fm_Empleados"
End Sub

Private Sub close_Click()
    DoCmd.Close

End Sub

Private Sub Form_Current()

On Error GoTo ErrHere
 If Not IsNumeric(Id.Value) Then
    ' Create capture operation.
    Set Capture = New dpfpCapture
    ' Start capture operation.
    Capture.StartCapture
    ' Create DPFPFeatureExtraction object.
    Set CreateFtrs = New DPFPFeatureExtraction
    ' Create DPFPEnrollment object.
    Set CreateTempl = New DPFPEnrollment
    ' Show number of samples needed.
    samples.Caption = CreateTempl.FeaturesNeeded
    ' Create DPFPSampleConversion object.
    Set ConvertSample = New DPFPSampleConversion
 Else
    samples.Caption = ""
 End If

Exithere:
    Exit Sub
ErrHere:
    MsgBox Err.Description
    Resume Exithere
    
End Sub
