# Icecast Log Parser
A simple parsing application to convert IceCast streaming server logs to the format needed for NPR/SoundExchange.  This will take a directory full of log files, strip out the relatvent information, and create one CSV 
## Quick Start
* Checkout/download this repository 
* Copy the sx_log_generator.exe and sx_log_generator.exe.config from the published directory to a 'working' directory
 
  For this example, we are using c:\icecast
  
* Download your logs from your service provider and unzip them into a directory under your 'working' directory.
 
  Lets say we are working on May, I would unzip the files into c:\icecast\may
  
* Open a command prompt and change to your working directory
    ```
    cd \icecast
    ```
* Run the following command
    ``` 
    log_generator source destination stream-id [source-url]
      source: Directory or file to process. Directories should end with a '\', or the source will be treated as a file.
      destination: Destination file name.
      stream-id: No spaces, usually call letters eg. KTTT, WTTTHD1, etc.
        source-url: The source url for your stream, used to determine if the line represents someone accessing your stream. Defaults to /stream.xspf.
  ```
