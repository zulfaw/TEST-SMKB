Imports System
Imports System.Collections.Generic
Imports System.Net.Http.Headers
Imports System.Net.Http
Imports System.Security
Imports System.Text
Imports System.Threading.Tasks
'Imports WebForm1.Models
Imports Newtonsoft.Json
Imports System.IO
Imports iTextSharp.text

Public Class ValuesService

    Private ReadOnly _myhttpClientNew As HttpClient = New HttpClient()

    Public Async Function Login(username As String, password As String) As Task(Of TokenResponseModel)
        Try
            Dim tokenUrl As String = String.Format("{0}Token", GlobalSMKBAPI.G_UrlApi)
            Dim postData As List(Of KeyValuePair(Of String, String)) = New List(Of KeyValuePair(Of String, String))()
            postData.Add(New KeyValuePair(Of String, String)("username", System.Net.WebUtility.UrlEncode(username)))
            postData.Add(New KeyValuePair(Of String, String)("password", password))
            postData.Add(New KeyValuePair(Of String, String)("grant_type", "password"))

            Dim content As HttpContent = New FormUrlEncodedContent(postData)

            Dim authenticationBytes As Byte() = Encoding.ASCII.GetBytes("myPanic:7567fghjfgh")
            _myhttpClientNew.DefaultRequestHeaders.Accept.Clear()
            _myhttpClientNew.DefaultRequestHeaders.Accept.Add(New MediaTypeWithQualityHeaderValue("application/xml"))

            _myhttpClientNew.DefaultRequestHeaders.Authorization = New AuthenticationHeaderValue("Basic", Convert.ToBase64String(authenticationBytes))
            _myhttpClientNew.DefaultRequestHeaders.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))
            Dim response As HttpResponseMessage = Await _myhttpClientNew.PostAsync(tokenUrl, content)
            response.EnsureSuccessStatusCode()
            Dim result As String = Await response.Content.ReadAsStringAsync()
            'Dim result As String = response.Content.ToString()

            Dim tokenResponse As TokenResponseModel = JsonConvert.DeserializeObject(Of TokenResponseModel)(result)
            Return tokenResponse
        Catch ex As Exception
            Throw New SecurityException("Bad credentials", ex)
        End Try
    End Function

    Public Async Function GetValuesStart2(accessToken As String, myStr As String) As Task(Of IEnumerable(Of String))
        Try
            Dim tokenUrl As String = String.Format("{0}api/" & myStr, GlobalSMKBAPI.G_UrlApi)
            _myhttpClientNew.DefaultRequestHeaders.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))
            _myhttpClientNew.DefaultRequestHeaders.Authorization = New AuthenticationHeaderValue("Bearer", accessToken)

            Dim response As HttpResponseMessage = Await _myhttpClientNew.GetAsync(tokenUrl)

            Dim result As String = Await response.Content.ReadAsStringAsync()
            Dim values As List(Of String) = JsonConvert.DeserializeObject(Of List(Of String))(result)
            Return values
        Catch ex As Exception
            Throw New SecurityException("error", ex)
        End Try
    End Function

    Public Async Function GetValuesStart(accessToken As String, myStr As String) As Task(Of IEnumerable(Of String))
        Try
            Dim tokenUrl As String = String.Format("{0}api/" & myStr, GlobalSMKBAPI.G_UrlApi)

            _myhttpClientNew.DefaultRequestHeaders.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))
            _myhttpClientNew.DefaultRequestHeaders.Authorization = New AuthenticationHeaderValue("Bearer", accessToken)

            Dim response As HttpResponseMessage = Await _myhttpClientNew.GetAsync(tokenUrl)

            Dim result As String = Await response.Content.ReadAsStringAsync()
            Dim values As List(Of String) = JsonConvert.DeserializeObject(Of List(Of String))(result)

            Return values
        Catch ex As Exception
            Throw New SecurityException("error", ex)
        End Try
    End Function

    Public Async Function SendDataAndStream(accessToken As String, File As Stream, data1 As String, data2 As String) As Task(Of String)
        Try
            Dim tokenUrl As String = String.Format("{0}api/Smkb/Uploadfill", GlobalSMKBAPI.G_UrlApi)
            _myhttpClientNew.DefaultRequestHeaders.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))
            _myhttpClientNew.DefaultRequestHeaders.Authorization = New AuthenticationHeaderValue("Bearer", accessToken)
            Dim formx As MultipartFormDataContent = New MultipartFormDataContent()
            formx.Add(New StringContent(data1), "data1")
            formx.Add(New StringContent(data2), "data2")
            If File IsNot Nothing Then
                Dim content As HttpContent = New StringContent("fileToUpload")
                formx.Add(content, "fileToUpload")
                Dim stream = File
                content = New StreamContent(stream)
                content.Headers.ContentDisposition = New ContentDispositionHeaderValue("form-data") With {
            .Name = "fileToUpload",
            .FileName = "myImage.jpg"
        }

                formx.Add(content)
            End If
            Dim response As HttpResponseMessage = Await _myhttpClientNew.PostAsync(tokenUrl, formx)
            Dim result As String = Await response.Content.ReadAsStringAsync()
            Return result
        Catch ex As Exception
            Throw New SecurityException("error", ex)
        End Try
    End Function

    Public Async Function SendData(accessToken As String, data1 As String, data2 As String) As Task(Of String)
        Try
            Dim tokenUrl As String = String.Format("{0}api/Smkb/Fill", GlobalSMKBAPI.G_UrlApi)
            _myhttpClientNew.DefaultRequestHeaders.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))
            _myhttpClientNew.DefaultRequestHeaders.Authorization = New AuthenticationHeaderValue("Bearer", accessToken)
            Dim formx As MultipartFormDataContent = New MultipartFormDataContent()
            formx.Add(New StringContent(data1), "data1")
            formx.Add(New StringContent(data2), "data2")

            Dim response As HttpResponseMessage = Await _myhttpClientNew.PostAsync(tokenUrl, formx)
            Dim result As String = Await response.Content.ReadAsStringAsync()
            Return result
        Catch ex As Exception
            Throw New SecurityException("error", ex)
        End Try
    End Function
    Public Async Function SendDataLejar_lama(accessToken As String, data1 As String, data2 As String, data3 As String, data4 As String, data5 As String, data6 As String, data7 As String, data8 As Double, data9 As String, data10 As String, data11 As String) As Task(Of String)
        Try
            Dim tokenUrl As String = String.Format("{0}api/Smkb/Fill_Lejar", GlobalSMKBAPI.G_UrlApi)
            _myhttpClientNew.DefaultRequestHeaders.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))
            _myhttpClientNew.DefaultRequestHeaders.Authorization = New AuthenticationHeaderValue("Bearer", accessToken)
            Dim formx As MultipartFormDataContent = New MultipartFormDataContent()
            formx.Add(New StringContent(data1), "data1")
            formx.Add(New StringContent(data2), "data2")
            formx.Add(New StringContent(data3), "data3")
            formx.Add(New StringContent(data4), "data4")
            formx.Add(New StringContent(data5), "data5")
            formx.Add(New StringContent(data6), "data6")
            formx.Add(New StringContent(data7), "data7")
            formx.Add(New StringContent(data8), "data8")
            formx.Add(New StringContent(data9), "data9")
            formx.Add(New StringContent(data10), "data10")
            formx.Add(New StringContent(data11), "data11")

            Dim response As HttpResponseMessage = Await _myhttpClientNew.PostAsync(tokenUrl, formx)
            Dim result As String = Await response.Content.ReadAsStringAsync()
            Return result
        Catch ex As Exception
            Throw New SecurityException("error", ex)
        End Try
    End Function

    Public Async Function SendDataLejar(accessToken As String, data1 As String, data2 As String, data3 As String, data4 As String, data5 As String, data6 As String, data7 As String, data8 As Double, data9 As String, data10 As String, data11 As String) As Task(Of String)
        Try
            Dim client As New HttpClient()
            Dim tokenUrl As String = String.Format("{0}api/Publicw/Fill_Lejar", "https://devmobile.utem.edu.my/smkb/")

            Dim formx As New MultipartFormDataContent()
            formx.Add(New StringContent(accessToken), "Ticket_id")

            formx.Add(New StringContent(data1), "data1")
            formx.Add(New StringContent(data2), "data2")
            formx.Add(New StringContent(data3), "data3")
            formx.Add(New StringContent(data4), "data4")
            formx.Add(New StringContent(data5), "data5")
            formx.Add(New StringContent(data6), "data6")
            formx.Add(New StringContent(data7), "data7")
            formx.Add(New StringContent(data8), "data8")
            formx.Add(New StringContent(data9), "data9")
            formx.Add(New StringContent(data10), "data10")
            formx.Add(New StringContent(data11), "data11")

            Dim response As HttpResponseMessage = Await client.PostAsync(tokenUrl, formx)
            Dim result As String = Await response.Content.ReadAsStringAsync()

            Return result
        Catch ex As Exception
            Throw New SecurityException("error", ex)
        End Try
    End Function
End Class

