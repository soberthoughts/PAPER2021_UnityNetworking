// Copyright Epic Games, Inc. All Rights Reserved.

#include "NetworkingUNREALGameMode.h"
#include "NetworkingUNREALCharacter.h"
#include "UObject/ConstructorHelpers.h"

ANetworkingUNREALGameMode::ANetworkingUNREALGameMode()
{
	// set default pawn class to our Blueprinted character
	static ConstructorHelpers::FClassFinder<APawn> PlayerPawnBPClass(TEXT("/Game/ThirdPersonCPP/Blueprints/ThirdPersonCharacter"));
	if (PlayerPawnBPClass.Class != NULL)
	{
		DefaultPawnClass = PlayerPawnBPClass.Class;
	}
}
