# SOMIOD - Systems Integration Academic Project

## Project Overview
**SOMIOD (Service-Oriented Middleware for Interoperability and Open Data)** is an academic project that focuses on building middleware to enable interoperability for IoT applications using a RESTful API, and includes testing applications for resource management and command simulation.

For more detailed information, refer to the [project statement](Projecto_Enunciado_2022-2023_Final.pdf).

### Key Features
- **Middleware**:
    - RESTful API to manage resources (`application`, `module`, `data`, `subscription`).
    - Full implementation of CRUD operations for all resource types.
    - XML data format for data persistence and transfer.
    - Persistence using a MySQL database.
    - Notifications via MQTT for resource subscriptions.

- **Testing Applications**:
    - `Test_Application`: Subscribe to applications, send commands to modules.
    - `App_Devices`: Create applications and modules, view received commands, and manage subscriptions.
    - Generalized simulation of IoT commands.

---

## Repository Structure
The project consists of three subprojects:

1. **API**:
    - Implements the middleware and provides endpoints for CRUD operations.

2. **App_Devices**:
    - Provides functionality to create applications and modules.
    - Displays module subscriptions and received commands.

3. **Test_Application**:
    - Allows subscription to applications and sending of commands to application modules.

---

## Technologies Used
- **Programming Language**: C#
- **Database**: MySQL
- **Development Environment**: Microsoft Visual Studio
- **Libraries and Frameworks**:
    - ASP.NET Web API
    - RestSharp
    - Newtonsoft.Json
    - MQTTnet

---

## Environment Setup
Ensure the following are installed:
- [Microsoft Visual Studio](https://visualstudio.microsoft.com/) with the following workloads:
    1. ASP.NET and web development
    2. .NET desktop development
- [Mosquitto MQTT Broker](https://mosquitto.org/download/) (install only; no additional configuration required).

### Step-by-Step Instructions
1. **Clone the Repository:**
   ```bash
   git clone https://github.com/Arcs1/Systems-Integration-Academic-Project
   ```

2. **Prepare XML Files:**
    - Navigate to the `Api/XML/XML_Files/` folder and make a copy of the file `applications.xml.example`, renaming it to `applications.xml`.
    - Navigate to the `Test_Application/CommandsXML/` folder and make a copy of the file `CommandsFile.xml.example`, renaming it to `CommandsFile.xml`.

---

## Notes
- This project was developed for academic purposes to showcase middleware development for IoT systems.
- Contributions are not accepted for this repository. It serves as a showcase of the work completed for the project.

---

## License
This project is unlicensed and is made publicly available **for showcase purposes only**.
