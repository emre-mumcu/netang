# Running Angular Application over https

Using mkcert to create self-signed certificates to use for https.

``` zsh
# Install mkcert using homebrew
% brew install mkcert
% brew install nss # if you use firefox

# To add the local CA to the system trust store: 
% mkcert -install 

# To create certificate and necessary files use the following command. This command will create 2 files localhost-key.pem and localhost.pem.
% mkcert localhost
```

Open the **angular.json** file and find the serve section. Create a property named options at the top of the serve section and include the following sub properties as follows:

``` json
"serve": {
    "options": {
        "ssl": true,
        "sslCert": "./ssl/localhost.pem",
        "sslKey": "./ssl/localhost-key.pem"
    },
    "builder": "...",
}
```

Then you can run the following:

``` zsh
% ng serve -o
```