Public Class clsRecipe
    ' Declare vars, set defaults
    Dim intRecipeID As Int32
    Dim strRecipeName As String
    Dim decBias As Decimal
    Dim intCurrentRange As Int32
    Dim intNPLC As Int32
    Dim strFilterType As String
    Dim intFilterCount As Int32
    Dim intSettlingTime As Int32
    Dim intReadingInterval As Int32
    Dim intMinimumReadings As Int32
    Dim strBath1Analyte As String
    Dim strBath2Analyte As String
    Dim strBath3Analyte As String
    Dim strBath4Analyte As String
    Dim strBath5Analyte As String
    Dim strBath6Analyte As String
    Dim intBathSource() As Int32 = {-1, -1, -1, -1, -1, -1}
    Dim intBathDestination() As Int32 = {-1, -1, -1, -1, -1, -1}

    Public Property RecipeID As Integer
        Get
            Return intRecipeID
        End Get
        Set(ByVal intValue As Integer)
            intRecipeID = intValue
        End Set
    End Property

    Public Property RecipeName As String
        Get
            Return strRecipeName
        End Get
        Set(ByVal strValue As String)
            strRecipeName = strValue
        End Set
    End Property

    Public Property BiasVoltage As Decimal
        Get
            Return decBias
        End Get
        Set(ByVal decValue As Decimal)
            decBias = decValue
        End Set
    End Property

    Public Property CurrentRange As Integer
        Get
            Return intCurrentRange
        End Get
        Set(ByVal intValue As Integer)
            intCurrentRange = intValue
        End Set
    End Property

    Public Property NPLC As Integer
        Get
            Return intNPLC
        End Get
        Set(ByVal intValue As Integer)
            intNPLC = intValue
        End Set
    End Property

    Public Property FilterType As String
        Get
            Return strFilterType
        End Get
        Set(ByVal strValue As String)
            strFilterType = strValue
        End Set
    End Property

    Public Property FilterCount As Integer
        Get
            Return intFilterCount
        End Get
        Set(ByVal intValue As Integer)
            intFilterCount = intValue
        End Set
    End Property

    Public Property SettlingTime As Integer
        Get
            Return intSettlingTime
        End Get
        Set(ByVal intValue As Integer)
            intSettlingTime = intValue
        End Set
    End Property

    Public Property ReadingInterval As Integer
        Get
            Return intReadingInterval
        End Get
        Set(ByVal intValue As Integer)
            intReadingInterval = intValue
        End Set
    End Property

    Public Property MinimumReadings As Integer
        Get
            Return intMinimumReadings
        End Get
        Set(ByVal intValue As Integer)
            intMinimumReadings = intValue
        End Set
    End Property

    Public Property Bath1Analyte As String
        Get
            Return strBath1Analyte
        End Get
        Set(ByVal strValue As String)
            strBath1Analyte = strValue
        End Set
    End Property
    Public Property Bath2Analyte As String
        Get
            Return strBath2Analyte
        End Get
        Set(ByVal strValue As String)
            strBath2Analyte = strValue
        End Set
    End Property
    Public Property Bath3Analyte As String
        Get
            Return strBath3Analyte
        End Get
        Set(ByVal strValue As String)
            strBath3Analyte = strValue
        End Set
    End Property
    Public Property Bath4Analyte As String
        Get
            Return strBath4Analyte
        End Get
        Set(ByVal strValue As String)
            strBath4Analyte = strValue
        End Set
    End Property
    Public Property Bath5Analyte As String
        Get
            Return strBath5Analyte
        End Get
        Set(ByVal strValue As String)
            strBath5Analyte = strValue
        End Set
    End Property
    Public Property Bath6Analyte As String
        Get
            Return strBath6Analyte
        End Get
        Set(ByVal strValue As String)
            strBath6Analyte = strValue
        End Set
    End Property

    Public Property BathSource(ByVal i As Integer) As Integer
        Get
            Return intBathSource(i)
        End Get
        Set(ByVal intValue As Integer)
            intBathSource(i) = intValue
        End Set
    End Property

    Public Property BathDestination(ByVal i As Integer) As Integer
        Get
            Return intBathDestination(i)
        End Get
        Set(ByVal intValue As Integer)
            intBathDestination(i) = intValue
        End Set
    End Property

    Public Sub New()
        ' In order for this object to be properly serialized by the XMLSerializer class, the constructor must be empty
    End Sub

End Class
