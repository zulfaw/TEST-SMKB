﻿Public Class Laporan_ABM
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim NowY As Date = Now.AddYears(1)
            txtTahun.Text = NowY.Year
        End If
    End Sub

End Class