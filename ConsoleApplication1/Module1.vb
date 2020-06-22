

Imports System.Globalization
Imports System.Text
Imports System.IO
Imports EAGetMail 'imports EAGetMail namespace

'added commment to test commit 27Apr20
'added comment to test set new system 22jun20
Module Module1

    Function _generateFileName(ByVal sequence As Integer) As String
        Dim currentDateTime As DateTime = DateTime.Now

        Return "test.txt"
        Return String.Format("{0}-{1:000}-{2:000}.txt",
                            currentDateTime.ToString("yyyyMMddHHmmss", New CultureInfo("en-US")),
                            currentDateTime.Millisecond,
                            sequence)
    End Function

    

    Sub Main()

        Try
            ' Create a folder named "inbox" under current directory
            ' to save the email retrieved.
            Dim localInbox As String = String.Format("{0}\inbox", Directory.GetCurrentDirectory())

            ' If the folder is not existed, create it.
            If Not Directory.Exists(localInbox) Then
                Directory.CreateDirectory(localInbox)
            End If
            Dim i As Integer
            i = 1

            Dim fileName As String = _generateFileName(i + 1)
            Dim fullPath As String = String.Format("{0}\{1}", localInbox, fileName)

            If System.IO.File.Exists(fullPath) = True Then

                Dim objWriter As New System.IO.StreamWriter(fullPath)

                objWriter.Write(DateTime.Now)
                objWriter.Close()


            Else

                'MessageBox.Show("File Does Not Exist")

            End If

            '' Gmail IMAP server is "imap.gmail.com"
            'Dim oServer As New MailServer("imap.gmail.com",
            '        "lssubbu1111@gmail.com",
            '        "",
            '        ServerProtocol.Imap4)

            '' Enable SSL/TLS connection, most modern email server require SSL/TLS connection by default.
            'oServer.SSLConnection = True
            '' Set 993 IMAP SSL Port
            'oServer.Port = 993

            'Console.WriteLine("Connecting server ...")

            'Dim oClient As New MailClient("TryIt")
            'oClient.Connect(oServer)

            'Dim infos As MailInfo() = oClient.GetMailInfos()
            'Console.WriteLine("Total {0} email(s)", infos.Length)

            'For i As Integer = 0 To 3

            '    ' Generate an unqiue email file name based on date time.
            '    Dim fileName As String = _generateFileName(i + 1)
            '    Dim fullPath As String = String.Format("{0}\{1}", localInbox, fileName)

            '    ' Save email to local disk
            '    FileSystem.FileCopy(fullPath, fullPath)


            'Next

            ' Quit and expunge emails marked as deleted from IMAP server.

            Console.WriteLine("Completed!")

        Catch ep As Exception
            Console.WriteLine(ep.Message)
        End Try

    End Sub
End Module