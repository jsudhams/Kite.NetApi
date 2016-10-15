Imports System.Security.Cryptography
Imports System.Text
Imports Newtonsoft.Json.Linq
Imports RestSharp

Public Class KiteApi

    ''' <summary>
    ''' PlaceOrder Funtion used to send order to Kite
    ''' </summary>
    ''' <param name="Order_Veriety"></param>
    ''' <param name="exchange">NSE/NFO/BSE/BFO/CDX/MCX/MCXSX</param>
    ''' <param name="tradingsymbol">Actual NSE/BSE Symbol like INFY</param>
    ''' <param name="transaction_type">BUY/SELL</param>
    ''' <param name="quantity">Quantity Integer</param>
    ''' <param name="price">Price in Decimal/Double</param>
    ''' <param name="product">NRML/MIS/CNC/CO</param>
    ''' <param name="order_type">MARKET/LIMIT/SL/SL-M</param>
    ''' <param name="validity">DAY/IOC/GTC/AMO</param>
    ''' <param name="disclosed_quantity">Quantity Integer</param>
    ''' <param name="trigger_price">Price in Decimal/Double</param>
    ''' <param name="squareoff_value">Price Difference in Integer??</param>
    ''' <param name="stoploss_value">Price in Difference Integer??</param>
    ''' <param name="trailing_stoploss">Price in Integer?? Note sure</param>
    ''' <returns></returns>

    Public Function Order_Place(Order_Veriety As String,
                                exchange As String,
                                tradingsymbol As String,
                                transaction_type As String,
                                quantity As Int64,
                                price As Decimal,
                                product As String,
                                order_type As String,
                                validity As String,
                                disclosed_quantity As Int64,
                                trigger_price As Decimal,
                                squareoff_value As Decimal,
                                stoploss_value As Decimal,
                                trailing_stoploss As Decimal) As String

        Dim OrderParams As New Dictionary(Of String, Object)
        OrderParams.Add("exchange", exchange)
        OrderParams.Add("tradingsymbol", tradingsymbol)
        OrderParams.Add("transaction_type", transaction_type)
        OrderParams.Add("quantity", quantity)
        OrderParams.Add("price", price)
        OrderParams.Add("product", product)
        OrderParams.Add("order_type", order_type)
        OrderParams.Add("validity", validity)
        OrderParams.Add("disclosed_quantity", disclosed_quantity)
        OrderParams.Add("trigger_price", trigger_price)
        OrderParams.Add("squareoff_value", squareoff_value)
        OrderParams.Add("stoploss_value", stoploss_value)
        OrderParams.Add("trailing_stoploss", trailing_stoploss)


        Return ExecOnKiteAPI("/orders/" & Order_Veriety, _ApiKey, _AccessToken, RestSharp.Method.POST, OrderParams)


    End Function

    Public Function Order_Modify_DONOTUSEYET(Order_Veriety As String,
                                exchange As String,
                                tradingsymbol As String,
                                transaction_type As String,
                                quantity As Int64,
                                price As Decimal,
                                product As String,
                                order_type As String,
                                validity As String,
                                disclosed_quantity As Int64,
                                trigger_price As Decimal,
                                squareoff_value As Decimal,
                                stoploss_value As Decimal,
                                trailing_stoploss As Decimal) As String

        Dim OrderParams As New Dictionary(Of String, Object)
        OrderParams.Add("exchange", exchange)
        OrderParams.Add("tradingsymbol", tradingsymbol)
        OrderParams.Add("transaction_type", transaction_type)
        OrderParams.Add("quantity", quantity)
        OrderParams.Add("price", price)
        OrderParams.Add("product", product)
        OrderParams.Add("order_type", order_type)
        OrderParams.Add("validity", validity)
        OrderParams.Add("disclosed_quantity", disclosed_quantity)
        OrderParams.Add("trigger_price", trigger_price)
        OrderParams.Add("squareoff_value", squareoff_value)
        OrderParams.Add("stoploss_value", stoploss_value)
        OrderParams.Add("trailing_stoploss", trailing_stoploss)


        Return ExecOnKiteAPI("/orders/" & Order_Veriety, _ApiKey, _AccessToken, RestSharp.Method.POST, OrderParams)


    End Function


    ''' <summary>
    ''' Cancels a particular order
    ''' </summary>
    ''' <param name="order_veriety">Specify the order variety like regular/amo/bo/co</param>
    ''' <param name="orderid">Order Id to cancel</param>
    ''' <param name="parent_orderid">For CO orders provide the order id first leg order</param>
    ''' <returns>JSON with status and Order ID if successful</returns>

    Public Function Order_Cancel(order_veriety As String, orderid As String, Optional parent_orderid As String = "") As String


        'DELETE the following format 
        'https://api.kite.trade/orders/regular/151220000000000?api_key=xxx&access_token=yyy

        Dim params As New Dictionary(Of String, Object)
        params.Add("parent_order_id", parent_orderid)

        Dim route As String
        If order_veriety = "BO" Then
            params.Add("parent_order_id", parent_orderid)
        End If

        route = "/orders/" & order_veriety & "/" & orderid



        Return ExecOnKiteAPI(route, _ApiKey, _AccessToken, Method.DELETE, params)



    End Function


    ''' <summary>
    ''' Gets all the orders for the day
    ''' </summary>
    ''' <returns>JSON String list of orders with status</returns>
    Public Function Order_GetList() As String


        'GET the orders list in the following format 
        'https://api.kite.trade/orders?api_key=xxx&access_token=yyy

        Dim params As New Dictionary(Of String, Object)

        Dim route As String

        route = "/orders"

        Return ExecOnKiteAPI(route, _ApiKey, _AccessToken, Method.GET, params)



    End Function

    Public Function Order_GetSingleOrderDetails(orderid As String) As String


        'GET the orders list in the following format 
        '"https://api.kite.trade/orders/151220000000000?api_key=xxx&access_token=yyy"

        Dim params As New Dictionary(Of String, Object)

        Dim route As String

        route = "/orders/" & orderid

        Return ExecOnKiteAPI(route, _ApiKey, _AccessToken, Method.GET, params)



    End Function

    ''' <summary>
    ''' Get positions of intraday, future and options, gets both day and net
    ''' </summary>
    ''' <returns>JSON String list of Positions with status</returns>
    Public Function Position_GetList() As String


        'GET the orders list in the following format 
        'https://api.kite.trade/portfolio/positions/?api_key=xxx&access_token=yyy

        Dim params As New Dictionary(Of String, Object)

        Dim route As String

        route = "/portfolio/positions"

        Return ExecOnKiteAPI(route, _ApiKey, _AccessToken, Method.GET, params)



    End Function

    Public Function Holding_GetList() As String


        'GET the orders list in the following format 
        '"https://api.kite.trade/portfolio/holdings/?api_key=xxx&access_token=yyy"

        Dim params As New Dictionary(Of String, Object)

        Dim route As String

        route = "/portfolio/holdings"

        Return ExecOnKiteAPI(route, _ApiKey, _AccessToken, Method.GET, params)



    End Function
    Public Function Quote_GetSingle(exchange As String, tradingSymbol As String) As String

        'Post the following format 
        'https://api.kite.trade/instruments/NSE/INFY?api_key=xxx&access_token=yyy


        Dim route As String
        route = "/instruments/" & exchange & "/" & tradingSymbol
        Dim params As New Dictionary(Of String, Object)


        Return ExecOnKiteAPI(route, _ApiKey, _AccessToken, Method.GET, params)

    End Function

    Private Function ExecOnKiteAPI(route As String, api_key As String, access_token As String, httpMethod As RestSharp.Method, params As Dictionary(Of String, Object)) As String
        Dim rc As New RestClient()
        rc.BaseUrl = New Uri("https://api.kite.trade")

        Dim req As New RestRequest(route, httpMethod)

        req.AddParameter("api_key", api_key)
        req.AddParameter("access_token", access_token)

        For Each item In params
            req.AddParameter(item.Key, item.Value)
        Next


        req.AddHeader("content-type", "application/x-www-form-urlencoded")

        Dim resp As RestResponse
        resp = rc.Execute(req)

        Dim content As String
        content = resp.Content
        rc = Nothing
        req = Nothing
        resp = Nothing
        Return content
    End Function


    Private Function EncryptSHA256Managed(ByVal ClearString As String) As String

        Dim bytClearString() As Byte = Encoding.UTF8.GetBytes(ClearString)
        Dim sha As New System.Security.Cryptography.SHA256Managed()
        Dim hash() As Byte = sha.ComputeHash(bytClearString)
        Dim checksum As String
        checksum = ""
        For Each x In hash
            checksum += String.Format("{0:x2}", x)
        Next

        Return checksum

    End Function

    Private Function EncryptSHA256Managed1(ByVal ClearString As String) As String

        Dim byteClearString() As Byte = Encoding.UTF8.GetBytes(ClearString)
        Dim sha As System.Security.Cryptography.SHA256Managed
        sha = SHA256Managed.Create()
        Dim hash() As Byte = sha.ComputeHash(byteClearString)
        Dim sb As New StringBuilder()

        Dim checksum As String
        checksum = ""
        For Each x In hash
            sb.Append(x.ToString("x2"))
        Next

        checksum = sb.ToString()

        Return checksum
    End Function

    Public Function GetAccessToken() As Integer

        Try

            _CheckSum = EncryptSHA256Managed(_ApiKey & _RequestToken & _ApiSecret)
            Dim jsonresponse As String

            jsonresponse = GetAuthTokens(_ApiKey, _RequestToken, _CheckSum)

            Dim jo = JObject.Parse(jsonresponse)

            _AccessToken = jo("data")("access_token")
            _PublicToken = jo("data")("public_token")
            Return 0
        Catch ex As Exception
            _ex = ex
            Return 0
        End Try
    End Function



    Private Function GetAuthTokens(api_key As String, request_token As String, checksum As String) As String
        Dim rc As New RestClient()
        rc.BaseUrl = New Uri("https://api.kite.trade")

        Dim req As New RestRequest("/session/token", Method.POST)

        req.AddParameter("api_key", api_key)
        req.AddParameter("request_token", request_token)
        req.AddParameter("checksum", checksum)
        req.AddHeader("content-type", "application/x-www-form-urlencoded")
        Dim resp As RestResponse
        resp = rc.Execute(req)

        Dim content As String
        content = resp.Content
        rc = Nothing
        req = Nothing
        resp = Nothing
        Return content
    End Function

    Private _ApiKey As String
    Public Property ApiKey() As String
        Get
            Return _ApiKey
        End Get
        Set(ByVal value As String)
            _ApiKey = value
        End Set
    End Property

    Private _ApiSecret As String
    Public Property ApiSecret() As String
        Get
            Return _ApiSecret
        End Get
        Set(ByVal value As String)
            _ApiSecret = value
        End Set
    End Property

    Private _AccessToken As String
    Public Property AccessToken() As String
        Get
            Return _AccessToken
        End Get
        Set(ByVal value As String)
            _AccessToken = value
        End Set
    End Property

    Private _PublicToken As String
    Public Property PublicToken() As String
        Get
            Return _PublicToken
        End Get
        Set(ByVal value As String)
            _PublicToken = value
        End Set
    End Property

    Private _RequestToken As String
    Public Property RequestToken() As String
        Get
            Return _RequestToken
        End Get
        Set(ByVal value As String)
            _RequestToken = value
        End Set
    End Property

    Private _CheckSum As String
    Public Property CheckSum() As String
        Get
            Return _CheckSum
        End Get
        Set(ByVal value As String)
            _CheckSum = value
        End Set
    End Property

    Private _zUserID As String
    Public Property zUserID() As String
        Get
            Return _zUserID
        End Get
        Set(ByVal value As String)
            _zUserID = value
        End Set
    End Property


    Private _ex As Exception
    Public ReadOnly Property Ex() As Exception
        Get
            Return _ex
        End Get
    End Property

    Private websocket As WebSocket4Net.WebSocket



    Public Function socketOpened(s As Object, e As EventArgs) As Integer
        websocket.Send("{ ""a"": ""subscribe"", ""v"": [408065]}")
        websocket.Send("{ ""a"": ""mode"", ""v"": [""ltp"",[408065]]}")
        Return 0
    End Function

    Private Function socketClosed(s As Object, e As EventArgs) As Integer
        websocket.Send("{ ""a"": ""unsubscribe"", ""v"": [408065]}")
        Return 0
    End Function

    Public Function socketError(s As Object, e As SuperSocket.ClientEngine.ErrorEventArgs) As Integer
        Return 0
    End Function

    Public Function socketMessage(s As Object, e As WebSocket4Net.MessageReceivedEventArgs) As Integer
        Return 0
    End Function



End Class
