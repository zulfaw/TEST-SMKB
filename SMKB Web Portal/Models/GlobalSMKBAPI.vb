Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
'Imports WebForm1.Models

Public Class GlobalSMKBAPI

    Public Shared G_UrlApi As String = "https://devmobile.utem.edu.my/smkb/"
    'public static string G_UrlApi = "https://mobileapp.utem.edu.my/"

    Public Class UserLoginDetails
        Public Shared Property u_Usertype As String
        Public Shared Property u_UsertypeB As String
        Public Shared Property u_Userid As String
        Public Shared Property u_Name As String
        Public Shared Property u_Email As String
        Public Shared Property u_Dept As String
        Public Shared Property u_Telephone As String
        Public Shared Property u_app_name As String
        Public Shared Property u_curr_version As String
        Public Shared Property u_app_url As String
        Public Shared Property u_reg_phoneid As String
        Public Shared Property u_reg_lockdate As String
        Public Shared Property u_reg_phoneappid As String
        Public Shared Property u_curr_status_staf As String
        Public Shared Property u_curr_status_student As String
        Public Shared Property _accessToken As TokenResponseModel
    End Class
End Class
