// Fill out your copyright notice in the Description page of Project Settings.


#include "PickUp.h"

APickUp::APickUp() {
	//tell unreal to replicate
	bReplicates = true;

	PrimaryActorTick.bCanEverTick = false;


}