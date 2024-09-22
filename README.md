# Application Overview

This application's purpose is to monitor your sensor data from IoT devices. 
To get started using this app, you have to first register. After registering, you can create an API key from the dashboard (which you can later replace or remove).

After creating your API key you need to create a sensor in the dashboard to be able to receive data from your device using the REST API (details later on).
Creating sensors in the dashboard is required as without them, the data you send will not be saved.

In the data visualisation page, you can create different charts (using Apex Charts: https://github.com/apexcharts/Blazor-ApexCharts), tables or a graph for binary data.
You need to give a sensor name for the chart/table/binary graph that corresponds with the data which you send e.g. you have two temperature sensors
that measure different temperature, you can give name temp1 and temp2 (or whatever you'd like) so they are displayed in different charts or tables.

You can delete readings data on sensor basis. If you delete a sensor, all the data corresponding with the sensor will also be deleted.
This means all the readings, charts and email alerts you've added. Deleting you API key will remove **everything** that is sensors, readings data, charts and email alerts.

The app is running on Azure: https://pk-iotcloud.azurewebsites.net/

# Creating a sensor

Creating a sensor is really simple. Head on to the dashboard, and assuming you have an API key created, you can press the button
to open a form to add a sensor. Choose your reading type (Distance, Humidity, Luminosity, Temperature and Velocity), give the sensor
a name and you're good to go. The sensor is added to a table so you can keep track of your sensors as you'll need the sensor name
for saving the data from your actual sensors/devices.

# Sending data

You can send five primary kind of data and binary data that corresponds to these five data types.
The types are Distance, Humidity, Luminosity, Temperature and Velocity. Next I'll overview the usage of the REST API.

## Distance

An example URL: ``https://pk-iotcloud.azurewebsites.net/api/Distance/add?apiKey={your_api_key}=&distance={distance_value}&sensorName={your_chosen_sensor_name}``

In this ``{your_api_key}`` is the same key you have created in the dashboard. ``{distance_value}`` is the data from your sensor and ``{your_chosen_sensor_name}`` should be the same
you use in your chart/table/binary graph so that your data from the correct sensor is shown.

The same for binary data would be: ``https://pk-iotcloud.azurewebsites.net/api/Distance/addBinary?apiKey={your_api_key}=&binary={binary_value}&sensorName={your_chosen_sensor_name}``

In this case, the URL is sligthly different with ``add`` from the beginning being replaced with ``addBinary`` and ``distance`` being replaced with ``binary``.
Binary value (as is the name) can only receive values 1 or 0. You can create a graph in the dashboard to display this value with 1 being a red circle and 0 a green circle.

The rest of the data follow similar pattern which I will give examples in their respective sections

## Humidity

Regular: ``https://pk-iotcloud.azurewebsites.net/api/Humidity/add?apiKey={your_api_key}=&humidity={humidity_value}&sensorName={your_chosen_sensor_name}``

Binary: ``https://pk-iotcloud.azurewebsites.net/api/Humidity/addBinary?apiKey={your_api_key}=&binary={binary_value}&sensorName={your_chosen_sensor_name}``

## Luminosity

Regular: ``https://pk-iotcloud.azurewebsites.net/api/Luminosity/add?apiKey={your_api_key}=&luminosity={luminosity_value}&sensorName={your_chosen_sensor_name}``

Binary: ``https://pk-iotcloud.azurewebsites.net/api/Luminosity/addBinary?apiKey={your_api_key}=&binary={binary_value}&sensorName={your_chosen_sensor_name}``

## Temperature

Regular: ``https://pk-iotcloud.azurewebsites.net/api/Temperature/add?apiKey={your_api_key}=&temperature={temperature_value}&sensorName={your_chosen_sensor_name}``

Binary: ``https://pk-iotcloud.azurewebsites.net/api/Temperature/addBinary?apiKey={your_api_key}=&binary={binary_value}&sensorName={your_chosen_sensor_name}``

## Velocity

Regular: ``https://pk-iotcloud.azurewebsites.net/api/Velocity/add?apiKey={your_api_key}=&velocity={velocity_value}&sensorName={your_chosen_sensor_name}``

Binary: ``https://pk-iotcloud.azurewebsites.net/api/Velocity/addBinary?apiKey={your_api_key}=&binary={binary_value}&sensorName={your_chosen_sensor_name}``
