#include <SPI.h>
#include <Adafruit_GFX.h>
#include <Adafruit_SSD1306.h>
#include <Wire.h>
#include <Adafruit_Sensor.h>
#include <Adafruit_AM2320.h>
#include <ESP8266WiFi.h>
#include <ESP8266WiFiMulti.h>

#include "SensorConfig.h"


void setup() {
  Serial.begin(115200);
  
  Serial.print("Configuring ");
  Serial.print(DEVICE_NAME);
  Serial.println("...");

  Wire.begin();

  if(SENSOR_CONNECTED){
    Serial.print("Initializing air sensor...");
    initSensor();
    Serial.println("Done");
  }

  if(DISPLAY_CONNECTED){
    Serial.print("Initializing display...");
    initDisplay();
    Serial.println("Done");
  }

  if(WIFI_CONNECTED){
    Serial.print("Initializing wi-fi connection...");
    initWifi();
    Serial.println("Done");
  }

  Serial.println("Device initialization completed.");
  Serial.println();
}

void loop() {
  if(SENSOR_CONNECTED){
    handleSensorReadings();
  }
  else{
    handleNoSensor();
  }
  delay(DELAY_MS);
}

#pragma region General

void handleSensorReadings(){
    float temp = getTemperature();
    float hum = getHumidity();
    String wifiStatus = getWifiConnectedStatus();

    if(DISPLAY_CONNECTED){
      displayData(temp, hum, wifiStatus);
    }

    if(WIFI_CONNECTED){
      sendDataToCloud(temp, hum);
    }

    printDataSerial(temp, hum, wifiStatus);  
}

void handleNoSensor(){
  Serial.print("I have no sensor, so I have no clue what to do");
}

void printDataSerial(float temp, float hum, String WifiStatus)
{
  Serial.print("Temperature: ");
  Serail.print(temp);
  Serial.println(" C");

  Serial.print("Humidity: ");
  Serail.print(hum);
  Serial.println(" %");

  Serial.print("Wifi status: ");
  Serail.println(Wifi);
  Serial.println();
  Serial.println();
}

#pragma endregion

#pragma region AM2320
Adafruit_AM2320 am2320 = Adafruit_AM2320();

void initSensor(){
  am2320.begin();
}

float getTemperature(){
  return am2320.readTemperature();
}

float getHumidity(){
  return am2320.readHumidity();
}
#pragma endregion

#pragma region Wifi

const char* ssid = STASSID;
const char* password = STAPSK;

ESP8266WiFiMulti WiFiMulti;

void initWifi(){
  WiFi.mode(WIFI_STA);
  WiFiMulti.addAP(ssid, password);

  Serial.print("Wait for WiFi... ");
  Serial.println("");
  Serial.println("WiFi connected");
  Serial.println("IP address: ");
  Serial.println(WiFi.localIP());

  while (WiFiMulti.run() != WL_CONNECTED) {
    Serial.print(".");
    delay(500);
  }
}

String getWifiConnectedStatus(){

  if(WIFI_CONNECTED){
    if(WiFi.isConnected()){
      return "OK";
    }
    
    return "No conn";

  }
  else{
    return "N/A";
  }

}

void sendDataToCloud(float temp, float hum){

  
}

#pragma endregion

#pragma region Display

Adafruit_SSD1306 display(SCREEN_WIDTH, SCREEN_HEIGHT, &Wire, OLED_RESET);

void initDisplay(){
  for(int cnt = 0; cnt<5;cnt++){
    if(cnt > 0){
      Serial.print("Retry #" + cnt.toString() + "...");
    }

    if(!display.begin(SSD1306_SWITCHCAPVCC, SCREEN_ADDRESS)) {
      

      Serial.println("Display initialization failed");
      if (cnt == 5){
        Serial.println("Last retry failed.. Device ")
        return;
      }

      cnt++;
    }
    else{
      return;
    }

  }
}

void displayData(float temp, float hum, String wifiStatus){
    displayPrepare();
    
    displaySingleDataLine("T: ", String(temp), " C");
    displayEmptyLine();
    displaySingleDataLine("H: ", String(hum), " %");  
    displayEmptyLine();
    displaySingleDataLine("Wifi: ", wifiStatus, "");

    display.display();
}

void displayPrepare(){
  display.clearDisplay();
  display.setTextSize(2);
  display.setCursor(0, 0);
  display.setTextColor(SSD1306_WHITE);
}

void displaySingleDataLine(String name, String value, String suffix){
  display.print(name); 
  display.print(value); 
  display.println(suffix);
}

void displayEmptyLine(){
  display.setTextSize(1);
  display.println();
  display.setTextSize(2);
}

#pragma endregion