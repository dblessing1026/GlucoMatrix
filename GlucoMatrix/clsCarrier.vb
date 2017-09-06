Option Explicit On
' ----------------------------------------------------------------------------------------
' The Carrier class defines an object to contain the lot number and carrier ID
' of the unit(s) under test.  Together they identify the lot and ID of a carrier.
' 
' The globally-instantiated Carrier object used in various modules to keep track of 
' the carriers moving in and out of the test system.
' -------------------------------------------------------------------------------------
Public Class clsCarrier
    ' Declare vars, set defaults
    Dim intCarrierSQLID As Integer = -1 ' the lot ID from tblLot of the SQL server. -1 will indicate "empty" lot
    Dim strCarrierText As String = "-1" ' the carrier ID of the carrier under test.
    Dim intUserID As Integer = -1



    Public Property CarrierSQLID As Integer
        Get
            Return intCarrierSQLID
        End Get
        Set(ByVal intValue As Integer)
            intCarrierSQLID = intValue
        End Set
    End Property
    Public Property CarrierText As String
        Get
            Return strCarrierText
        End Get
        Set(ByVal strValue As String)
            strCarrierText = Left(strValue, 2)
        End Set
    End Property
    Public Property UserID As Integer
        Get
            Return intUserID
        End Get
        Set(ByVal intValue As Integer)
            intUserID = intValue
        End Set
    End Property

End Class
