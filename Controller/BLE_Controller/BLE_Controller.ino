#include <BLEDevice.h>
#include <BLEUtils.h>
#include <BLEServer.h>
#include <BLE2902.h>
// See the following for generating UUIDs:
// https://www.uuidgenerator.net/

#define SERVICE_UUID "4fafc201-1fb5-459e-8fcc-c5c9c331914b"
#define CHARACTERISTIC_UUID "beb5483e-36e1-4688-b7f5-ea07361b26a8"
#define CHARACTERISTIC_UUIDa "617c753e-5199-11eb-ae93-0242ac130002"


bool deviceConnected = false;
class MyServerCallbacks : public BLEServerCallbacks {
  void onConnect(BLEServer *pServer) {
    deviceConnected = true;
  };

  void onDisconnect(BLEServer *pServer) {
    deviceConnected = false;
  }
};


BLECharacteristic *pCharacteristic;
BLECharacteristic *pCharacteristica;

// constants won't change
#define LIGHT_SENSOR_PIN 36  // ESP32 pin GPIO36 (ADC0) connected to light sensor
#define LED_PIN 17           // 
#define ANALOG_THRESHOLD 1500

void setup() {
  pinMode(LED_PIN, OUTPUT);  // set ESP32 pin to output mode
  Serial.begin(9600);

  BLEDevice::init("AllBark");
  BLEServer *pServer = BLEDevice::createServer();
  pServer->setCallbacks(new MyServerCallbacks());
  BLEService *pService = pServer->createService(SERVICE_UUID);

  pCharacteristic = pService->createCharacteristic(
    CHARACTERISTIC_UUID,
    BLECharacteristic::PROPERTY_READ | BLECharacteristic::PROPERTY_WRITE | BLECharacteristic::PROPERTY_NOTIFY | BLECharacteristic::PROPERTY_INDICATE);

  pCharacteristica = pService->createCharacteristic(
    CHARACTERISTIC_UUIDa,
    BLECharacteristic::PROPERTY_READ | BLECharacteristic::PROPERTY_WRITE | BLECharacteristic::PROPERTY_NOTIFY | BLECharacteristic::PROPERTY_INDICATE);

  pCharacteristica->addDescriptor(new BLE2902());

  pCharacteristic->addDescriptor(new BLE2902());
  pService->start();
  pServer->getAdvertising()->start();
}

void loop() {

  int analogValue = analogRead(LIGHT_SENSOR_PIN);  // read the value on analog pin
  //digitalWrite(LED_PIN, HIGH); // turn on LED
  //Serial.println(analogValue);
  if (deviceConnected) {
    Serial.println("Device Connected");
    if (analogValue == 0) {
      int i = 0b0001;
      Serial.println(i);
      pCharacteristic->setValue(i);
      pCharacteristic->notify();
    }
    if (analogValue > 1) {
      int i = 0b0010;
      Serial.println(i);
      pCharacteristic->setValue(i);
      pCharacteristic->notify();
    }
  }
}
