[![Build][build-shield]][build-url]
[![MIT License][license-shield]][license-url]
[![LinkedIn][linkedin-shield]][linkedin-url]

<h1 align="center">
	<b>ADMINISTRATIVE DOCUMENTS</b>
	<br />
	<small align="center">API</small>
</h1>

<details open="open">
  <summary>Table of Contents</summary>
<!-- TOC depthfrom:2 -->

-   [About The Project](#about-the-project)
    -   [Routes](#routes)
    -   [Build with](#build-with)
-   [Getting Started](#getting-started)
    -   [Prerequisites](#prerequisites)
    -   [Installation](#installation)
-   [Roadmap](#roadmap)
-   [License](#license)
-   [Contact](#contact)

<!-- /TOC -->
</details>

## About The Project

Web application allowing to store, in a secure way, administrative files (Back End).

### Routes

-   **Auth**
    -   Authenticate : _Allows to authenticate with the couple email and password_
    -   Refresh : _Allows to refresh the authentication token with the cookie_
-   **Documents**
    -   Create : _Allows you to add a new document_
    -   Find : _Allows to have the details of a document_
    -   Latest : _Allows you to have the latest documents added_
    -   Download : _Allows to download the document_
    -   Search : _Allows you to search for documents_
-   **Document Types**
    -   Get All : _Allows to retrieve all the tags of the authenticated user_
    -   Find : _Allows to have the details of a tag_
-   **Document Tags**
    -   Get All : _Allows to retrieve all the tags of the authenticated user_
    -   Find : _Allows to display the details of a tag_
    -   Find By Slug : _Allows to display the details of a tag with the slug_

### Build with

-   [NET Core 5.0](https://dotnet.microsoft.com)
-   [EntityFramework Core](https://github.com/dotnet/efcore)
-   [MediatR](https://github.com/jbogard/MediatR)
-   [AutoMapper](https://github.com/AutoMapper/AutoMapper)
-   [AutoWrapper](https://github.com/proudmonkey/AutoWrapper)
-   [bcrypt.net](https://github.com/BcryptNet/bcrypt.net)
-   [FluentValidation](https://github.com/FluentValidation/FluentValidation)

## Getting Started

### Prerequisites

-   [NET Core 5.0](https://dotnet.microsoft.com/download/dotnet/5.0)

### Installation

1. Clone the repo
    ```sh
    git clone https://github.com/KristenJestin/administrative-documents-api.git
    ```
2. Migrate database
    ```sh
    dotnet ef database update
    ```
3. Start app

    ```sh
    dotnet run
    ```

## Roadmap

All future features are available on [Trello](https://trello.com/b/RldA4clM/%F0%9F%93%84-administrative-documents).

## License

Distributed under the MIT License. See `LICENSE` for more information.

<!-- CONTACT -->

## Contact

Kristen JESTIN - [contact@kristenjestin.fr](mailto:contact@kristenjestin.fr)

Project Link: [https://github.com/KristenJestin/administrative-documents-api](https://github.com/KristenJestin/administrative-documents-api)

<!-- MARKDOWN LINKS & IMAGES -->

[build-shield]: https://img.shields.io/github/workflow/status/KristenJestin/administrative-documents-api/CI?style=for-the-badge
[build-url]: https://github.com/KristenJestin/administrative-documents-api/actions?query=CI
[license-shield]: https://img.shields.io/github/license/KristenJestin/administrative-documents-api.svg?style=for-the-badge
[license-url]: https://github.com/KristenJestin/administrative-documents-api/blob/master/LICENSE
[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=for-the-badge&logo=linkedin&colorB=555
[linkedin-url]: https://linkedin.com/in/kristen-jestin
