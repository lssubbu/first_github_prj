Imports System.Globalization
Imports System.Text
Imports System.IO
Imports EAGetMail 'imports EAGetMail namespace

'added commment to test commit 27Apr20
Module Module1

    Function _generateFileName(ByVal sequence As Integer) As String
        Dim currentDateTime As DateTime = DateTime.Now
        Return String.Format("{0}-{1:000}-{2:000}.eml",
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

            ' Gmail IMAP server is "imap.gmail.com"
            Dim oServer As New MailServer("imap.gmail.com",
                    "lssubbu1111@gmail.com",
                    "",
                    ServerProtocol.Imap4)

            ' Enable SSL/TLS connection, most modern email server require SSL/TLS connection by default.
            oServer.SSLConnection = True
            ' Set 993 IMAP SSL Port
            oServer.Port = 993

            Console.WriteLine("Connecting server ...")

            Dim oClient As New MailClient("TryIt")
            oClient.Connect(oServer)

            Dim infos As MailInfo() = oClient.GetMailInfos()
            Console.WriteLine("Total {0} email(s)", infos.Length)

            For i As Integer = 0 To infos.Length - 1
                Dim info As MailInfo = infos(i)
                Console.WriteLine("Index: {0}; Size: {1}; UIDL: {2}",
                        info.Index, info.Size, info.UIDL)

                ' Retrieve email from IMAP server
                Dim oMail As Mail = oClient.GetMail(info)

                Console.WriteLine("From: {0}", oMail.From.ToString())
                Console.WriteLine("Subject: {0}" & vbCr & vbLf, oMail.Subject)

                ' Generate an unqiue email file name based on date time.
                Dim fileName As String = _generateFileName(i + 1)
                Dim fullPath As String = String.Format("{0}\{1}", localInbox, fileName)

                ' Save email to local disk
                oMail.SaveAs(fullPath, True)

                ' Mark email as deleted from IMAP server.
                oClient.Delete(info)

            Next

            ' Quit and expunge emails marked as deleted from IMAP server.
            oClient.Quit()
            Console.WriteLine("Completed!")

        Catch ep As Exception
            Console.WriteLine(ep.Message)
        End Try

    End Sub
End Module