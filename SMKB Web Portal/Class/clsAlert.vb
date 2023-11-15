Imports Microsoft.VisualBasic

Public Class clsAlert
    Public Enum AlertType
        Plain
        Success
        Information
        Warning
        Err
        'Primary
    End Enum

    Public Shared Sub Alert(MsgLabel As Label, Message As String, Optional MessageType As AlertType = AlertType.Plain,
                                     Optional Dismissable As Boolean = False)
        Dim style, icon As String
        Select Case MessageType
            Case AlertType.Plain
                style = "default"
                icon = ""
            Case AlertType.Success
                style = "success"
                icon = "check-circle"
            Case AlertType.Information
                style = "info"
                icon = "info-circle"
            Case AlertType.Warning
                style = "warning"
                icon = "exclamation-circle"
            Case AlertType.Err
                style = "danger"
                icon = "exclamation-triangle"
                'Case AlertType.Primary
                '    style = "primary"
                '    icon = "info"
        End Select

        If (Not MsgLabel.Page.IsPostBack Or MsgLabel.Page.IsPostBack) And Message = Nothing Then
            MsgLabel.Visible = False
        Else
            MsgLabel.Visible = True
            MsgLabel.CssClass = "alert alert-" & style & If(Dismissable = True, " alert-dismissible fade in font2", "")
            MsgLabel.Text = "<i class='fa fa-" & icon & " fa-lg'></i>  " & Message
            If Dismissable = True Then
                MsgLabel.Text &= "<button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true' color='red'>&times;</span></button>"
            End If
            MsgLabel.Focus()
            Message = ""
        End If
    End Sub


End Class
