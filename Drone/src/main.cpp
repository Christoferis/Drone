#include <Arduino.h>
#include <MPU6050.h>
#include <coms.h>
#include <motor_control.h>
#include <double3.h>
// Drone Script for controlling and getting input of Mobile

//sensor
MPU6050 sensor(0x68);
double3 offset;

double3 getRotation(){
  double3 rotation;

  rotation.x = sensor.getRotationX(); 
  rotation.y = sensor.getRotationY();
  rotation.z = sensor.getRotationZ();

  //subtract offset from it
  return rotation - offset;
}

void setup() {
  //init calibrate the sensor
  sensor.initialize();
  offset = getRotation();

  Serial.begin(9600);
}

void loop() {
  // put your main code here, to run repeatedly:
  double3 rot = getRotation();

  Serial.println(rot.x);
  Serial.println(rot.y);
  Serial.println(rot.z);
  Serial.println(sensor.testConnection());
}

//get stuff