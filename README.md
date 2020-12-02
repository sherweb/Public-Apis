![Master](https://github.com/sherweb/Public-Apis/workflows/Master/badge.svg)

# Sherweb public APIs

[Version française](LISEZMOI.md)

## Prerequisite for using the APIs

You need to have added an application with API keys within the Sherweb partner portal: https://cumulus.sherweb.com. On the left menu, under Security, click on the APIs menu:

![Menu in partner portal to create API keys](docs/ApiKeysMenu.png)

Add a new application. Make sure to copy the *Client Id*, *Client Secret* and *Subscription Key* which are required in order to use the APIs:

![Windows to copy over all the required information to connect to the APIs](docs/ApiInformations.png)

## NuGet packages source code

We have an organization on nuget.org: https://www.nuget.org/profiles/Sherweb. All public NuGet packages are there. This repository provides all the source codes for these NuGet packages if you want to build them on your own instead of using built packages. Each folder under **SourceCode** is an API and there is a Readme for each. To build the code, you will need to open the .sln file in Visual Studio.

## Sample code

In the folder **SampleCode**, you will find all the examples on how to use the NuGet packages. All examples are in C#. This is where you will need the *Client Id*, *Client Secret* and *Subscription Key*. Feel free to submit any code enhancements to us.

## Postman
In the folder **Postman**, you will find all files allowing you to test the APIs directly with [Postman](https://www.postman.com/). There is a "Collection" for each API plus an "Environement". Here's a quick how-to-use :
1. Set your *ClientId*, *ClientSecret* and *SubscriptionKey* in the environment.
2. Use the Authorization collection to retrieve your token.
3. Set this token in the "Authorization" section of each API call (replace YOUR_TOKEN by your token).
4. Voilà.

## Documentation

You can find all the documentation related to these APIs here: https://developers.sherweb.com/.

## Contributing

[You can submit code related bugs and feature requests](https://github.com/sherweb/Public-Apis/issues), and help us verify as they are checked in.

## Support Requests

You can send support requests from the  [Sherweb partner portal](https://cumulus.sherweb.com/nexus/redirect/support?ticket=new).

## License

Copyright (c) Sherweb Inc. All rights reserved.
Licensed under the [MIT](LICENSE.txt) license.
