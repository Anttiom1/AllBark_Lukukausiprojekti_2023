#include <BLEDevice.h>
#include <BLEUtils.h>
#include <BLEServer.h>
#include <BLE2902.h>
#include <Adafruit_MPU6050.h>
#include <Adafruit_Sensor.h>
#include <Wire.h>
// See the following for generating UUIDs:
// https://www.uuidgenerator.net/

#define SERVICE_UUID "4fafc201-1fb5-459e-8fcc-c5c9c331914b"
#define CHARACTERISTIC_UUID "beb5483e-36e1-4688-b7f5-ea07361b26a8"
#define CHARACTERISTIC_UUIDa "617c753e-5199-11eb-ae93-0242ac130002"

Adafruit_MPU6050 mpu;
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

  if (!mpu.begin()) {
    Serial.println("Failed to find MPU6050 chip");
    while (1) {
      delay(10);
    }
  }
  Serial.println("MPU6050 Found!");

  mpu.setGyroRange(MPU6050_RANGE_500_DEG);
  Serial.print("Gyro range set to: ");
  switch (mpu.getGyroRange()) {
    case MPU6050_RANGE_250_DEG:
      Serial.println("+- 250 deg/s");
      break;
    case MPU6050_RANGE_500_DEG:
      Serial.println("+- 500 deg/s");
      break;
    case MPU6050_RANGE_1000_DEG:
      Serial.println("+- 1000 deg/s");
      break;
    case MPU6050_RANGE_2000_DEG:
      Serial.println("+- 2000 deg/s");
      break;
  }

  mpu.setFilterBandwidth(MPU6050_BAND_5_HZ);
  Serial.print("Filter bandwidth set to: ");
  switch (mpu.getFilterBandwidth()) {
    case MPU6050_BAND_260_HZ:
      Serial.println("260 Hz");
      break;
    case MPU6050_BAND_184_HZ:
      Serial.println("184 Hz");
      break;
    case MPU6050_BAND_94_HZ:
      Serial.println("94 Hz");
      break;
    case MPU6050_BAND_44_HZ:
      Serial.println("44 Hz");
      break;
    case MPU6050_BAND_21_HZ:
      Serial.println("21 Hz");
      break;
    case MPU6050_BAND_10_HZ:
      Serial.println("10 Hz");
      break;
    case MPU6050_BAND_5_HZ:
      Serial.println("5 Hz");
      break;


  }



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

  sensors_event_t a, g, temp;
  mpu.getEvent(&a, &g, &temp);

  //Serial.print("Rotation X: ");
  //Serial.print(g.gyro.x);
  //Serial.print(", Y: ");
  //Serial.print(g.gyro.y);
  //Serial.print(", Z: ");
  //Serial.print(g.gyro.z);
  //Serial.println(" rad/s");
  int i = 0;
  int x = g.gyro.x * 10;
  int y = g.gyro.y * 10;
  int z = g.gyro.z * 10;
  //Serial.println(x);
  i = i<<8;
  i |= x;
  i = i<<8;
  i |= y;
  i = i<<8;
  i |= z;
  Serial.print("X:");
  Serial.println(x);
  Serial.print("Y:");
  Serial.println(y);
  Serial.print("Z:");
  Serial.println(z);
  Serial.print("i BIN: ");
  Serial.println(i, BIN);
  delay(100);


  int analogValue = analogRead(LIGHT_SENSOR_PIN);  // read the value on analog pin
  digitalWrite(LED_PIN, HIGH);                     // turn on LED
  //Serial.println(analogValue);
  if (deviceConnected) {
    //Serial.println("Device Connected");
    


    //if (analogValue > 1500) {
    //int i = 0b0010;
    //Serial.println(i);
    //pCharacteristic->setValue(i);
    //pCharacteristic->notify();
  }
}

