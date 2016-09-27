# Kite.NetApi
Kite Api for .Net users


How to use this?
================

1. Go to KiteApi\bin\Release folder and download the dlls/xml files
2. Reference the  KiteApi.dll from above downloaded files
3. Ensure Newtonsoft.Json.dll and RestSharp.dll are in same folder as KiteApi.dll
4. Start using the Api
4. Since the login requires manual login to Kite you need deal with it in UI.. See the Forms vb.net sample below.

Upcoming
Will be adding WebSocket streaming to this soon in a week or two

[Release notes]
[Date: 28/Sep/2016  Update: Intial relase]
In this release it is just a 


[Sample login part]

' The bkow code should be ibn you application global part or module/class that can be access across app
Public kapi As KiteApi.KiteApi

'Main form
Public Class Main


    Private Sub btnConnect_Click(sender As Object, e As EventArgs) Handles btnConnect.Click

        ConnectAndLogin()

    End Sub
    
    Private Sub ConnectAndLogin()


        Dim url As String

        url = "https://kite.trade/connect/login?api_key=xxxx"

        BrowserFrom.Show()
        BrowserFrom.Hide()

        BrowserFrom.wb.ScriptErrorsSuppressed = True
        BrowserFrom.wb.Navigate(New Uri(url))
        BrowserFrom.ShowDialog()
        kapi.GetAccessToken()
        if you got up to this then login is successful
        
        Dim AuthTokens As String
        'Store your tokens for use later in app, you need to login only once in a day to kite api to get these
        
        AuthTokens = "{""logindate"":""" & (Format(Now(), "dd/MMM/yyyy")) & """,""apikey"":""" & kapi.ApiKey & """, ""apisecret"":""" & kapi.ApiSecret & """, ""zuserid"":""" & kapi.zUserID & """, ""access_token"":""" & kapi.AccessToken & """ , ""public_token"":""" & kapi.PublicToken & """}"

        
        
        'In the brwoser form you need capture the request_token in redirect after login
    End Sub
End Class

    'Below is code of the browser form to capture request_token
    
    Public Class BrowserFrom
      Private Sub wb_Navigated(sender As Object, e As WebBrowserNavigatedEventArgs) Handles wb.Navigated
          Dim SessionUrl As String

          SessionUrl = wb.Url.ToString

          Dim RequestToken As String
          RequestToken = Web.HttpUtility.ParseQueryString(SessionUrl)("request_token")
          kapi.RequestToken = RequestToken
          If kapi.RequestToken <> "" Then
              Me.Close()
          End If

      End Sub
    End Class
