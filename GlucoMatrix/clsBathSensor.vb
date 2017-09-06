Option Explicit On
' ----------------------------------------------------------------------------------------
' The BathSensor class defines an object to contain/convert between sequence number, bath,
' and sensor.  Together they convert to/from sequence to bath and sensor.
' 
' The globally-instantiated BathSensor object used in various modules to keep track of 
' the carriers moving in and out of the test system.
' -------------------------------------------------------------------------------------
Public Class clsBathSensor
    ' Declare vars, set defaults
    Dim intSequence As Integer = 1 ' Sequence number, typically 1 to 96
    Dim intBath As Integer = 1     ' the bath corresponding to the sequence number
    Dim intSensor As Integer = 1   ' the sensor within a carrier/bath corresponding to the bath or sequence number



    Public Property SequenceNumber As Integer
        Get
            Return intSequence
        End Get
        Set(ByVal intValue As Integer)
            intSequence = intValue
            intSensor = intValue Mod 16

            If intSensor = 0 Then
                intBath = (intValue \ 16)
                intSensor = 16
            Else
                intBath = (intValue \ 16) + 1
            End If
        End Set
    End Property
    Public Property Bath As Integer
        Get
            Return intBath
        End Get
        Set(ByVal intValue As Integer)
            intBath = intValue
            intSequence = (intBath - 1) * 16 + intSensor
        End Set
    End Property
    Public Property Sensor As Integer
        Get
            Return intSensor
        End Get
        Set(ByVal intValue As Integer)
            intSensor = intValue
            intSequence = (intBath - 1) * 16 + intSensor
        End Set
    End Property
End Class
