# RepoXRay - Medical Image Analysis and Consultation Platform

## Project Description
RepoXRay is a comprehensive interface application developed under the TÜBİTAK 2209-A project at the Artificial Intelligence Laboratory of İzmir Katip Çelebi University (İKÇÜ). It enables automatic disease and anomaly detection from CT and X-Ray images (using the YOLOv11n ONNX model representatively), generates medically compliant reports, and facilitates doctor-patient interaction. It also serves as an alternative desktop interface for the MedRAX (Medical Reasoning Agent) system.

## Features
- **Firebase Authentication:** Secure login system.
- **YOLOv11n ONNX Model:** Disease and anomaly detection in medical images (used representatively).
- **Automated Reporting:** Generation of medically compliant reports.
- **Report Sharing:** Download as PDF or send to other doctors.
- **Statistical Analysis:** Health statistics for patients and doctors.
- **Social Media-like Interaction:** Consultation and information sharing among doctors.
- **MedRAX Integration:** Alternative desktop interface for the MedRAX (Medical Reasoning Agent) system.

## Technologies
- .NET 8
- Firebase
- SQLite
- YOLOv11n ONNX Model (used representatively)

## Installation
1. Clone the repository:
   ```bash
   git clone https://github.com/batuaribakir/RepoXRay-MedRAX-Interface.git
## Notes
- FireBaseHelper.cs file removed for security reasons
- Drop file functionallity should be improved
- Ddit profile functionality should be improved
- Folder bindings and designs should be improved
- After logging out, the system is entered with any text. This error should be corrected
- convert report to PDF or Word functionality should be improved
- Multi-user feature should be added
