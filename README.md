# File Transfer Website Using .Net Core

The above code is for a website that is currently being hosted on the following link: http://nathanmizzi-001-site1.etempurl.com

This website aims to imitate the functionality provided by weTransfer, which essentially helps its users when transferring a file.

The website requires two emails to function: The email of the person sending the transfer and the email of the person recieving the transfer. A File is also required,
optionally, the user may specify a password, if this is done the file is zipped with a password that is sent alongside the email with the file. 

Please note, the above website will only send emails to users registered with the mailgun account specified in the fileTransferController,
in order to adapt it to your own needs you would need to create your own account, and change the api key spedified in the controller to your own.
