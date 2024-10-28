# Filename init.sh used as entrypoint, put it in your .net core root # folder
mkdir -p $HOME/.pki/nssdb
certutil -d sql:$HOME/.pki/nssdb -N --empty-password
pk12util -i Cert/empty-certificate.p12 -d sql:$HOME/.pki/nssdb -W 'YOURPASSWORD'
Xvfb :99 -screen 0 640x480x8 -nolisten tcp &
dotnet Selenium.Docker.Certificate.dll