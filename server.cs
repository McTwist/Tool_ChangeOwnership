// ================= 
// * Change Ownership
// * Changes ownership of a stack of bricks
// =================
// * Author: McTwist(9845)
// * Date: 2016 Nov 18
// =================
// * Please note that for normal users this add-on is used
// * with trust. You do not give build trust to someone
// * you do not trust. The author of this add-on takes no
// * responsibilities for anything that relates to what
// * this add-on may do to you or anyone you have given
// * trust to.
// =================

// ============
// * Includes *
// ============

exec("./namedtargets.cs");

// ===============
// * Preferences *
// ===============

// BlocklandGlass
if ($BLPrefs::Init)
{
	registerPref("Tool_ChangeOwnership", "", "Admin Command",           "bool", "$Pref::Server::CO::AdminCommand",          false, "",        "", false, false, false);
	registerPref("Tool_ChangeOwnership", "", "Force Create BrickGroup", "bool", "$Pref::Server::CO::ForceCreateBrickGroup",  true, "",        "", false, false, false);
	registerPref("Tool_ChangeOwnership", "", "Queue speed",             "int",  "$Pref::Server::CO::QueueSpeed",              100, "1 1000",  "", false, false, true);
	registerPref("Tool_ChangeOwnership", "", "Transfer speed",          "int",  "$Pref::Server::CO::TransferSpeed",          1000, "1 10000", "", false, false, true);
}
// RTB
else if ($RTB::Hooks::ServerControl)
{
	RTB_registerPref("Admin Command",           "Change Ownership", "$Pref::Server::CO::AdminCommand",           "bool",        "Tool_ChangeOwnership", false, false, false, "");
	RTB_registerPref("Force Create BrickGroup", "Change Ownership", "$Pref::Server::CO::ForceCreateBrickGroup",  "bool",        "Tool_ChangeOwnership",  true, false, false, "");
	RTB_registerPref("Queue speed",             "Change Ownership", "$Pref::Server::CO::QueueSpeed",             "int 1 1000",  "Tool_ChangeOwnership",   100, false, true,  "");
	RTB_registerPref("Transfer speed",          "Change Ownership", "$Pref::Server::CO::TransferSpeed",          "int 1 10000", "Tool_ChangeOwnership",  1000, false, true,  "");
}
// Default
if ($Pref::Server::CO::AdminCommand $= "")          $Pref::Server::CO::AdminCommand          = false;
if ($Pref::Server::CO::ForceCreateBrickGroup $= "") $Pref::Server::CO::ForceCreateBrickGroup =  true;
if ($Pref::Server::CO::QueueSpeed $= "")            $Pref::Server::CO::QueueSpeed            =   100;
if ($Pref::Server::CO::TransferSpeed $= "")         $Pref::Server::CO::TransferSpeed         =  1000;

// ===================
// * Item datablocks *
// ===================

datablock itemData(ChownItem)
{
	cameraMaxDist   = 0.1;
	canDrop         = 1;
	category        = "Weapon";
	className       = "Tool";
	colorShiftColor = "0 0 1 1";
	density         = 0.2;
	doColorShift    = 1;
	elasticity      = 0.2;
	emap            = 1;
	friction        = 0.6;
	iconName        = "base/client/ui/itemIcons/wand";
	image           = ChownImage;
	shapeFile       = "base/data/shapes/wand.dts";
	uiName          = "Chown ";
};
datablock particleData(ChownExplosionParticle)
{
	colors[0]          = "0.3 0.3 1 0.9";
	colors[1]          = "0 0.2 1 0.7";
	colors[2]          = "0.3 0.4 1 0.5";
	gravityCoefficient = 0;
	lifetimeMS         = 400;
	lifetimeVarianceMS = 200;
	sizes[0]           = 0.6;
	sizes[1]           = 0.4;
	sizes[2]           = 0.3;
	spinRandomMax      = 90;
	spinRandomMin      = -90;
	textureName        = "base/data/particles/ring";
	times[1]           = 0.8;
	times[2]           = 1;
};

datablock particleEmitterData(ChownExplosionEmitter)
{
	ejectionOffset   = 0.5;
	ejectionPeriodMS = 4;
	ejectionVelocity = 3;
	particles        = ChownExplosionParticle;
	periodVarianceMS = 2;
	thetaMax         = 180;
	velocityVariance = 0;
};
datablock explosionData(ChownExplosion)
{
	camShakeDuration = 0.5;
	camShakeFreq     = "1 1 1";
	emitter[0]       = ChownExplosionEmitter;
	faceViewer       = 1;
	lifetimeMS       = 180;
	lightEndRadius   = 0.5;
	lightStartColor  = "0 0.7 1 1";
	lightStartRadius = 1;
	shakeCamera      = 1;
	soundProfile     = "wandHitSound";
};
datablock projectileData(ChownProjectile)
{
	bounceElasticity = 0;
	bounceFriction   = 0;
	explodeOnDeath   = 1;
	explosion        = ChownExplosion;
	fadeDelay        = 2;
	gravityMod       = 0;
	lifetime         = 0;
	mask             = $TypeMasks::FxBrickAlwaysObjectType | $TypeMasks::InteriorObjectType | $TypeMasks::TerrainObjectType | $TypeMasks::PlayerObjectType;
	range            = 10;
};
datablock particleData(ChownParticleA)
{
	colors[0]          = "0.0 0.6 1 0.9";
	colors[1]          = "0 0.2 1 0.7";
	colors[2]          = "0.3 0.4 1 0.5";
	gravityCoefficient = -0.5;
	lifetimeMS         = 400;
	lifetimeVarianceMS = 200;
	sizes[0]           = 0.1;
	sizes[1]           = 0.4;
	sizes[2]           = 0.6;
	spinRandomMax      = 90;
	spinRandomMin      = -90;
	textureName        = "base/data/particles/ring";
	times[1]           = 0.8;
	times[2]           = 1;
};

datablock particleEmitterData(ChownEmitterA)
{
	ejectionOffset   = 0.09;
	ejectionPeriodMS = 50;
	ejectionVelocity = 0.2;
	particles        = ChownParticleA;
	periodVarianceMS = 2;
	thetaMax         = 180;
	velocityVariance = 0;
};
datablock particleData(ChownParticleB)
{
	colors[0]          = "0.0 0.6 1 0.9";
	colors[1]          = "0 0.2 1 0.7";
	colors[2]          = "0.3 0.4 1 0.5";
	gravityCoefficient = -0.4;
	dragCoefficient    = 2;
	lifetimeMS         = 400;
	lifetimeVarianceMS = 200;
	sizes[0]           = 0.4;
	sizes[1]           = 0.6;
	sizes[2]           = 0.9;
	spinRandomMax      = 0;
	spinRandomMin      = 0;
	textureName        = "base/client/ui/brickIcons/1x1";
	times[1]           = 0.5;
	times[2]           = 1;
};

datablock particleEmitterData(ChownEmitterB)
{
	ejectionOffset   = -0.0;
	ejectionPeriodMS = 10;
	ejectionVelocity = 0;
	particles        = ChownParticleB;
	periodVarianceMS = 2;
	thetaMin		 = 0.0;
	thetaMax         = 0.1;
	velocityVariance = 0;
	orientParticles  = true;
	phiVariance		 = 10;
};
datablock shapeBaseImageData(ChownImage : wandImage)
{
	projectile = ChownProjectile;
	showBricks = true;
	colorShiftColor = "0.000000 0.000000 1.000000 1.000000";
	item = ChownItem;
	stateEmitter[1] = ChownEmitterA;
	stateEmitter[3] = ChownEmitterB;
};

function ChownImage::onStopFire(%this, %player)
{
	%player.stopThread(2);
}

function ChownImage::onFire(%this, %player)
{
	%player.playThread(2, armAttack);

	// Get from where player is looking
	%mask = $TypeMasks::FxBrickAlwaysObjectType | $TypeMasks::PlayerObjectType;
	%point = %player.getEyePoint();
	%obj = containerRayCast(%point, vectorAdd(vectorScale(vectorNormalize(%player.getEyeVector()), 4), %point), %mask, %player);

	// Require a valid object
	if (!isObject(%obj))
		return;

	// "Art is an explosion"
	%p = new Projectile()
	{
		datablock = %this.projectile;
		initialPosition = restWords(%obj);
	};
	%p.explode();

	%client = %player.client;
	// Create Chown
	if (!isObject(%client.chown))
		%client.chown = Chown(%client);

	if (%client.chown.isWorking())
		return;

	// Selected a player
	if (%obj.getType() & $TypeMasks::PlayerObjectType)
	{
		%bl_id = firstWord(%obj).client.bl_id;
		%client.chown.setTarget(%bl_id);
	}
	// Select a brick
	else if (%obj.getType() & $TypeMasks::FxBrickAlwaysObjectType)
	{
		%brick = firstWord(%obj);
		%client.chown.setStartBrick(%brick);
	}
}

package ChangeOwnershipPackage
{
	function ChownImage::onUnMount(%this, %player, %slot)
	{
		if (isObject(%player.client.chown))
		{
			%player.client.chown.delete();
			%player.client.chown = "";
		}
		bottomPrint(%player.client, "");
	}
};
activatePackage(ChangeOwnershipPackage);

// ============
// * Commands *
// ============

// Command to either get item or set target
function ServerCmdChown(%client, %bl_id)
{
	// Require a player spawned
	if (!isObject(%player = %client.player))
		return;

	if (isObject(%client.chown) && %client.chown.isWorking())
		return;

	// Limiting command to Super Admins
	if ($Pref::Server::CO::AdminCommand && !%client.isSuperAdmin)
		return;

	// Default command
	if (%bl_id $= "")
	{
		// Inform the user
		if (%player.getMountedImage(0) == ChownImage.getId())
		{
			messageClient(%client, '', "\c6Hit a brick and player or use \c3/chown \c6[\c1BL_ID\c6] to transfer bricks.");
		}
		// Give item to player
		else
		{
			%player.updateArm(ChownImage);
			%player.mountImage(ChownImage, 0);
		}
		return;
	}

	// Create Chown
	if (!isObject(%client.chown))
		%client.chown = Chown(%client);

	%client.chown.setTarget(%bl_id);
}

// ================
// * Chown object *
// ================

// Chown initiator
function Chown(%client)
{
	%this = new ScriptObject()
	{
		class = Chown;
		limit_bricks = $Pref::Server::CO::QueueSpeed;
		limit_transfer = $Pref::Server::CO::TransferSpeed;
	};
	%this.setClient(%client);
	return %this;
}

// Chown destructor
function Chown::onRemove(%this)
{
	if (isObject(%this.client))
		%this.client.chown = "";
	%this.clearCache();
}

// Clear cache
function Chown::clearCache(%this)
{
	deleteVariables("$Chown" @ %this @ "_*");
}

// Check if it is currently working
function Chown::isWorking(%this)
{
	return isEventPending(%this.event);
}

// Set the client using this object
function Chown::setClient(%this, %client)
{
	%this.client = %client;
	%client.chown = %this;
}

// Set starting brick
function Chown::setStartBrick(%this, %brick)
{
	if (isEventPending(%this.event))
		return;
	%this.brick = %brick;
	%this.source_group = %this.brick.getGroup();

	// Get all bricks desired to be selected
	%this.getBricks();
}

// Set target user
function Chown::setTarget(%this, %bl_id)
{
	if (isEventPending(%this.event))
		return;
	%this.bl_id = %bl_id;
	%this.target_group = "BrickGroup_" @ %bl_id;
	// Automatic start
	if (isObject(%this.brick))
		%this.transfer();
	// Let user know about next step
	else if (isObject(%this.client))
		%this.info("\c6Hit a brick to transfer all connected bricks");
}

// Get bricks from current brick
function Chown::getBricks(%this)
{
	%source_group = %this.brick.getGroup();

	// Check access right
	// You either need to be at least super admin
	// Or
	// You need to own the bricks
	if (!%this.client.isSuperAdmin &&
		(getTrustLevel(%source_group, %this.client) <= 2))
		return %this.info("\c3This player does not trust you enough.");

	%this.clearCache();

	// Visited bricks
	$Chown[%this, "V", %this.brick] = true;
	$Chown[%this, "V"] = 0;

	// Queued bricks
	$Chown[%this, "Q"] = 0;
	$Chown[%this, "Q", $Chown[%this, "Q"]++] = %this.brick;

	%this.tickGetBricks(0);
}

// Tick next batch of bricks to get
function Chown::tickGetBricks(%this, %i)
{
	cancel(%this.event);
	%this.event = "";

	for (%limit = 0; %limit < %this.limit_bricks && %i < $Chown[%this, "Q"]; %limit++)
	{
		// Get next brick
		%brick = $Chown[%this, "Q", %i++];

		// *wink*
		%this.blink(%brick);

		// Check up
		%count = %brick.getNumUpBricks();
		for (%n = 0; %n < %count; %n++)
		{
			%next = %brick.getUpBrick(%n);
			// Avoid touching this again
			if ($Chown[%this, "V", %next])
				continue;
			%next_group = %next.getGroup();
			// Is the target group to switch owner to
			if (%next_group != %this.source_group)
				continue;

			// Visited this one
			$Chown[%this, "V", %next] = true;
			$Chown[%this, "V"]++;

			// Put into queue
			$Chown[%this, "Q", $Chown[%this, "Q"]++] = %next;
		}

		// Check down
		%count = %brick.getNumDownBricks();
		for (%n = 0; %n < %count; %n++)
		{
			%next = %brick.getDownBrick(%n);
			// Avoid touching this again
			if ($Chown[%this, "V", %next])
				continue;
			%next_group = %next.getGroup();
			// Is the target group to switch owner to
			if (%next_group != %this.source_group)
				continue;

			// Visited this one
			$Chown[%this, "V", %next] = true;
			$Chown[%this, "V"]++;

			// Put into queue
			$Chown[%this, "Q", $Chown[%this, "Q"]++] = %next;
		}
	}

	// Notice the client
	if (isObject(%this.client))
		%this.info("\c3Queuing...");

	// Next one
	if (%i < $Chown[%this, "Q"])
		%this.event = %this.schedule(30, tickGetBricks, %i);
	// Finished
	else
		%this.finishedGetBricks();
}

// Finished getting the bricks
function Chown::finishedGetBricks(%this)
{
	if (isObject(%this.client))
		bottomPrint(%this.client, "\c3Queued: \c6[\c3" @ $Chown[%this, "Q"] @ " bricks\c6]", 2);

	// Automatic start
	if (%this.bl_id !$= "" && %this.bl_id >= 0 && %this.bl_id <= 999999)
		%this.transfer();
	// Let user know about next step
	else if (isObject(%this.client))
		%this.info("\c6Hit a user or use \c3/chown \c6[\c4bl_id\c6] to transfer all connected bricks.");
}

// Start a transfer of bricks
function Chown::transfer(%this)
{
	// Necessary checks
	if (!isObject(%this.brick))
		return;
	if (%this.bl_id $= "" || !(%this.bl_id >= 0 && %this.bl_id <= 999999))
		return;
	if (!isObject(%this.client))
		return;
	if (isEventPending(%this.event))
		return;

	%source_group = %this.brick.getGroup();

	// Same owner
	if (%source_group.bl_id == %this.bl_id)
		return %this.info("\c3Player already owns those bricks.");

	// Check if target group exists
	%target_group = "BrickGroup_" @ %this.bl_id;

	if (!isObject(%target_group))
	{
		if (!$Pref::Server::CO::ForceCreateBrickGroup)
			return %this.info("\c3Unable to find the target.");
		// We're done here
		if (!%this.client.isSuperAdmin)
			return %this.info("\c3You need to be a \c0Super Admin\c3.");

		// Create a new brickgroup
		new SimGroup(%target_group)
		{
			bl_id = %this.bl_id;
			name = "\c1BL_ID: " @ %this.bl_id @ "\c1\c0";
			client = 0;
		};
		mainBrickGroup.add(%target_group);
	}

	// Check access right
	// You either need to be at least super admin
	// Or
	// 1. You need to own the bricks
	// 2. You need to have build rights with target
	if (!%this.client.isSuperAdmin &&
		(getTrustLevel(%source_group, %this.client) <= 2 ||
		getTrustLevel(%source_group, %target_group) <= 0))
		return %this.info("\c3Insufficient trust.");

	%this.tickTransfer(0);
}

// Tick next batch of bricks to be transferred
function Chown::tickTransfer(%this, %brick_i)
{
	cancel(%this.event);
	%this.event = "";

	%queue_count = $Chown[%this, "Q"];
	for (%limit = 0; %limit < %this.limit_transfer && %brick_i < %queue_count; %limit++)
	{
		%brick = $Chown[%this, "Q", %brick_i++];

		// Reset name
		%name = %brick.getName();
		%brick.clearNTObjectName();
		%brick.setName("");

		// Move it to the new group
		%this.target_group.add(%brick);
		%brick.client = %this.target_group.client;
		%brick.stackBL_ID = %this.bl_id;

		// Set the name back
		%brick.setNTObjectName(%name);

		// TODO: Move this out in its own loop, reducing load significantly

		// Handle spawn bricks
		if (%brick.dataBlock.getId() == brickSpawnPointData.getId())
		{
			// Remove from old group
			%n = 0;
			%count = %this.source_group.spawnBrickCount;
			for (%i = 0; %i < %count; %i++)
			{
				if (%this.source_group.spawnBrick[%i] == %brick)
				{
					%this.source_group.spawnBrick[%i] = %this.source_group.spawnBrick[%count - 1];
					%this.source_group.spawnBrick[%count - 1] = "-1";
					%this.source_group.spawnBrickCount--;
					break;
				}
			}

			// Add to new group
			%this.target_group.spawnBrickCount <<= 0;
			%this.target_group.spawnBrick[%this.target_group.spawnBrickCount] = %brick;
			%this.target_group.spawnBrickCount++;
		}
	}

	// Notice the client
	if (isObject(%this.client))
		%this.info("\c3Transferring...");

	// Next one
	if (%brick_i < %queue_count)
		%this.event = %this.schedule(30, tickTransfer, %brick_i);
	// Finished
	else
		%this.finishedTransfer();
}

// Finished transfer bricks
function Chown::finishedTransfer(%this)
{
	if (isObject(%this.client))
		%this.info("\c3Finished.");

	%this.delete();
}

// Visually tell the user the brick was marked
function Chown::blink(%this, %brick)
{
	// Modify current event
	if (isEventPending(%brick.highlightSelected))
	{
		cancel(%brick.highlightSelected);
		// Fix someone edited it
		if (%brick.getColorFXID() != 3)
		{
			%brick.originalColorFX = %brick.getColorFXID();
			%brick.setColorFX(3);
		}
	}
	else
	{
		%brick.originalColorFX = %brick.getColorFXID();
		%brick.setColorFX(3);
	}
	%brick.highlightSelected = %brick.schedule(2000, setColorFX, %brick.originalColorFX);
}

// Tell user a bunch of things
function Chown::info(%this, %msg)
{
	if (!isObject(%this.client))
		return;

	%output = "";
	%ex = "";
	// Special message
	if (%msg !$= "")
	{
		%output = %output @ %msg;
		%ex = "\n";
	}
	// Got bricks
	if ($Chown[%this, "Q"] != 0)
	{
		%output = %output @ %ex @ "\c6[\c3" @ $Chown[%this, "Q"] @ " bricks\c6]";
		%ex = " ";
	}
	// Got target
	if (isObject(%this.target_group))
	{
		%output = %output @ %ex @ "\c6[Target: \c4" @ %this.target_group.name @ "\c6]";
		%ex = "";
	}

	bottomPrint(%this.client, %output);
}
