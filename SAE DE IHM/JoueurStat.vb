Imports System.Windows.Forms.AxHost

Public Module JoueurStat

    Public Structure StatJoueur
        Private _nomJ As String
        Private _NbrMaxCarre As Integer
        Private _TempsMinTrv As Integer
        Private _NbrPartieJouer As Integer
        Private _TempsTotal As Integer
        Private _Monnaie As Integer
        Private _ptsFlow As Integer
        Private _Hard As Boolean
        Public Sub New(nomJ As String, nbrMaxCarre As Integer, tempsMinTrv As Integer, nbrPartieJouer As Integer, tempsTotal As Integer, Monnaie As Integer, ptsFlow As Integer, Hard As Boolean)
            _nomJ = nomJ
            _NbrMaxCarre = nbrMaxCarre
            _TempsMinTrv = tempsMinTrv
            _NbrPartieJouer = nbrPartieJouer
            _TempsTotal = tempsTotal
            _Monnaie = Monnaie
            _ptsFlow = ptsFlow
            _Hard = Hard
        End Sub
        Public ReadOnly Property Tout As (NomJ As String, NbrMaxCarre As Integer, TempsMinTrv As Integer, NbrPartieJouer As Integer, TempsTotal As Integer, Monnaie As Integer, ptsFlow As Integer, Hard As Boolean)
            Get
                Return (_nomJ, _NbrMaxCarre, _TempsMinTrv, _NbrPartieJouer, _TempsTotal, _Monnaie, _ptsFlow, _Hard)
            End Get
        End Property


    End Structure
    Public carreTrv As Integer = 0
    Public TempsDernCarreTrv As Integer = Integer.MaxValue

    Private Joueur As New Dictionary(Of String, StatJoueur)

    Public Sub MAJStat(nom As String, carreTrv As Integer, tempsPartie As Integer, TempsDernCarreTrv As Integer)
        Dim stat As StatJoueur

        If Joueur.ContainsKey(nom) Then
            stat = Joueur(nom)
        Else
            stat = New StatJoueur(nom, 0, Integer.MaxValue, 0, 0, 0, 0, False)
        End If
        Dim nbrMaxCarre As Integer = stat.Tout.NbrMaxCarre
        Dim tempsMinTrv As Integer = stat.Tout.TempsMinTrv
        Dim nbrPartieJouer As Integer = stat.Tout.NbrPartieJouer
        Dim tempsTotal As Integer = stat.Tout.TempsTotal
        Dim Monnaie As Integer = stat.Tout.Monnaie
        Dim ptsFlow As Integer = stat.Tout.ptsFlow
        Dim Hard As Boolean = stat.Tout.Hard

        If carreTrv > nbrMaxCarre Then
            nbrMaxCarre = carreTrv
            tempsMinTrv = TempsDernCarreTrv
        ElseIf carreTrv = nbrMaxCarre And TempsDernCarreTrv < tempsMinTrv Then
            tempsMinTrv = TempsDernCarreTrv
        End If

        nbrPartieJouer += 1
        tempsTotal += tempsPartie
        Monnaie += carreTrv * 30
        If carreTrv = 5 Then
            Monnaie += (60 - TempsDernCarreTrv) * 2
        End If

        stat = New StatJoueur(nom, nbrMaxCarre, tempsMinTrv, nbrPartieJouer, tempsTotal, Monnaie, ptsFlow, Hard)

        Joueur(nom) = stat
    End Sub
    Public Sub MAJMonnaie(nom As String, MonnaieDepense As Integer)
        Dim stat As StatJoueur
        If Joueur.ContainsKey(nom) Then
            stat = Joueur(nom)
        Else
            stat = New StatJoueur(nom, 0, Integer.MaxValue, 0, 0, 0, 0, False)
        End If
        Dim Monnaie As Integer = stat.Tout.Monnaie
        If Monnaie < MonnaieDepense Then
            MsgBox("Force à toi t'es pauvre")
        Else
            Monnaie -= MonnaieDepense
        End If
        stat = New StatJoueur(nom, stat.Tout.NbrMaxCarre, stat.Tout.TempsMinTrv, stat.Tout.NbrPartieJouer, stat.Tout.TempsTotal, Monnaie, stat.Tout.ptsFlow, stat.Tout.Hard)

        Joueur(nom) = stat
    End Sub
    Public Sub MajHard(nom As String)
        Dim stat As StatJoueur
        stat = Joueur(nom)
        Dim Hard = stat.Tout.Hard
        Hard = True
        stat = New StatJoueur(nom, stat.Tout.NbrMaxCarre, stat.Tout.TempsMinTrv, stat.Tout.NbrPartieJouer, stat.Tout.TempsTotal, stat.Tout.Monnaie, stat.Tout.ptsFlow, Hard)
        Joueur(nom) = stat

    End Sub
    Sub main()
        Application.Run(frmAccueil)
        'Application.Run(frmJeu)

        'Application.Exit()
    End Sub

    Public ReadOnly Property Recup() As List(Of StatJoueur)
        Get
            Return Joueur.Values.ToList()
        End Get
    End Property
End Module
