Option Explicit On
Imports System.IO
Imports System.Net.Sockets
Imports System.Data.SqlClient


Module modGlobalVariables
    ' -----------------------------------------------------------------------------------------------------------
    ' The modGlobalVariables, as the name suggests, is a collection of global variables for use in all objects 
    ' and forms
    ' -----------------------------------------------------------------------------------------------------------


    'Constants
    Public Const strConfigFileName As String = "Config.xml"
    Public Const strApplicationName As String = "GlucoMatrix"
    Public Const strApplicationVersion As String = "0.0.3.4"
    Public Const strApplicationDescription As String = "Software for CGM Sensor release testing"
    ' The admin password to unlock the configuration settings is hardcoded.  In the future
    ' it may be desireable to incorporate database-driven user authentication / authorization for granular permissions
    Public strAdminPassword As String = "C0balt22"

    'Hardware Test Global Variables
    Public strHardwareErrorList As String 'Lists Hardware Errors 

    'Configuration Variables
    Public cfgGlobal As New clsConfiguration ' Global inexstance of the Configuration object that stores config information for the current session
    Public cfgRecipe As New clsRecipe

    'Carrier Variable
    'The strategy of these variables is to track the carriers under test (lstCarrier), the carriers coming
    'in (listCarrierIncoming), and the transformed list that will be the carriers under test after a hop
    '(lstCarrierNextPosition).  Two integer arrays track the baths incoming carriers will be moved to (intIncomingPosition),
    'and the movement of carriers through the batch (intNextPosition).
    'Example: Standard 6 level testing, moving from run-in to 1st level, then moving through 5 more levels
    '         would be intNextPosition = {2, 3, 4, 5, 6, -1} and intIncomingPosition = {1, -1, -1, -1, -1, -1}.
    '         This indicates a single carrier from run-in would move to position 1, position 1 to position 2,
    '         etc, position 5 to position 6, and position 6 moves out of the system.
    'Example: Three levels of testing, carriers move in pairs through the system.
    '         intNextPosition = {3, 4, 5, 6, -1, -1} and intIncomingPosition = {1, 2, -1, -1, -1, -1}
    'Note:    Default is 6 levels, single carrier moves
    Public lstCarrierIncoming As New List(Of clsCarrier)
    Public lstCarrierCurrent As New List(Of clsCarrier)               'Current carriers in the slots/baths
    Public lstCarrierNextPosition As New List(Of clsCarrier)   'Where the carriers will be after moving them
    Public intNextPosition() As Integer = {2, 3, 4, 5, 6, -1}   'The carrier moves to execute on a hop
    Public intIncomingPosition() As Integer = {1, -1, -1, -1, -1, -1} 'The carriers to move in on a hop


    'File/Directory Variables
    Public strAppDir As String = My.Application.Info.DirectoryPath ' The path to the application install directory
    Public fCurrentTestFile As File
    Public swLogFile As StreamWriter   'StreamWriter for log file, to remain open until test is complete


    'Status Variables

    Public boolConfigStatus As Boolean
    Public boolIOStatus As Boolean
    Public boolLogFile As Boolean = False

    Public boolIsTestRunning As Boolean = False ' Boolean flag - has the user clicked start and not yet clicked stop
    Public boolRunTest As Boolean = False      ' Boolean flag to trigger ending the test

    Public boolMoveCarriers As Boolean = False  ' Boolean flag to trigger moving carriers before the next reading is started


    'Test Variables
    Public strTestID As String
    Public strSensorIDHeader As String
    Public strCarrier1 As String
    Public strCarrier2 As String
    Public strTestType As String

    'SQL Server Variables
    Public strSQLServerAddress As String = "CGMDBLESING7450\STA"
    Public strDatabase As String = "glucomatrix"


    '===============================
    'Primary Key IDs for SQL Objects    Note: These may not need to be public, but I'm lazy - DB 19Jul2017
    '===============================

    'Primary Key ID for System Switch
    Public intSwitchID As Int32

    'Primary Key ID for Source Meter
    Public intSourceMeterID As Int32

    'Primary Key IDs for Switch Cards
    Public intCard1ID As Int32
    Public intCard2ID As Int32
    Public intCard3ID As Int32
    Public intCard4ID As Int32
    Public intCard5ID As Int32
    Public intCard6ID As Int32

    'Primary Key IDs for Card Counts
    Public intCard1CountsID As Int32
    Public intCard2CountsID As Int32
    Public intCard3CountsID As Int32
    Public intCard4CountsID As Int32
    Public intCard5CountsID As Int32
    Public intCard6CountsID As Int32

    'Primary Key for Test Parameters
    Public intParametersID As Int32

    'Primary Key for Test Configuration
    Public intTestConfigID As Int32

    'Primary Key for the CommonChannel "Lot"
    '   Note: The common channel will be read during each interval.  It will be assigned to the
    '   lot "CommonChannel" which will be a reserved Lot number.  With each change in configuration,
    '   a new "CommonChannel" lot will be created, so the primary key is not fixed.  This will allow
    '   the data to be pulled by day/run.
    Public intCommonChannelID As Int32

    'Primary Key for UserID
    Public intUserID As Int32
    Public boolIsAdmin As Boolean
    Public strCurrentUser As String

    'Primary Key for Recipe Paramaters and parameters






End Module
