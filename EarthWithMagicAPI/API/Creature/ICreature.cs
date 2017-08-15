﻿using EarthWithMagicAPI.API.Stuff;
using EarthWithMagicAPI.API.Util;
using EarthWithMagicAPI.API.Interfaces.Items;
using System;
using System.Collections.Generic;
using EarthWithMagicAPI.API.Interfaces.Spells;

namespace EarthWithMagicAPI.API.Creature
{
    /// <summary>
    /// Used to store and compare alignments.
    /// </summary>
    public enum Alignment
    {
        LawfulGood, NeutralGood, ChaoticGood, LawfulNeutral, TrueNeutral, ChaoticNeutral, LawfulEvil, NeutralEvil, ChaoticEvil
    }

    /// <summary>
    /// The race of the creature.
    /// </summary>
    public enum Race
    {
        Human, Elf, Dwarf, Kender, Drow, Duergar, HalfElf, Dragonborn, Planeswalker, Unspecified
    }

    /// <summary>
    /// Used to store and compare genders.
    /// </summary>
    public enum Gender
    {
        Male, Female, Unspecified
    }

    /// <summary>
    /// Holds various things that every creature has or should implement.
    /// </summary>
    public abstract class ICreature
    {
        /// <summary>
        /// The constructor for the ICreature abstract class.
        /// </summary>
        /// <param name="gender"></param>
        /// <param name="race"></param>
        /// <param name="alignment"></param>
        public ICreature(Gender gender, Race race, Alignment alignment)
        {
            this.Attributes.Gender = gender;
            this.Attributes.Race = race;
            this.Attributes.Alignment = alignment;
        }

        public int XPValue()
        {
            return ExperienceCalculator.Calculate(this);
        }

        /// <summary>
        /// The attributes for this creature.
        /// </summary>
        public CreatureAttributes Attributes = new CreatureAttributes();

        /// <summary>
        /// The amount of casting power the creature has after resting.
        /// </summary>
        public int MaxCastingPower = 0;

        /// <summary>
        /// The amount of casting power the creature currently has.
        /// </summary>
        public int CastingPower = 0;

        /// <summary>
        /// A list of abilities the creature has.
        /// </summary>
        public List<IAbility> ClassAbilities = new List<IAbility>();

        /// <summary>
        /// A list of all spells known to the creature.
        /// </summary>
        public List<ISpell> SpellsKnown = new List<ISpell>();

        /// <summary>
        /// A list of the spells that this creature can actually use right now.
        /// </summary>
        public List<ISpell> UsableSpells = new List<ISpell>();

        /// <summary>
        /// If true, the creature is part of the player's party.
        /// </summary>
        public bool IsInParty = false;

        /// <summary>
        /// The fists of the creature. Mainly used in combat by monks.
        /// </summary>
        public Fists BareHands = new Fists();

        /// <summary>
        /// The abilities of the creature.
        /// </summary>
        public CreatureAbilities Abilities = new CreatureAbilities();

        /// <summary>
        /// The title of the creature, if any.
        /// </summary>
        public string Title;

        /// <summary>
        /// The creature's name, if it has one.
        /// </summary>
        public string Name;

        /// <summary>
        /// The name of the type of creature the creature is.
        /// </summary>
        public string CreatureType;

        /// <summary>
        /// The unique ID of the creature.
        /// </summary>
        public Guid ID = new Guid();

        /// <summary>
        /// The amount of weight the creature can hold.
        /// </summary>
        public int WeightCapacity;

        /// <summary>
        /// Returns if the creature is hostile to the player.
        /// </summary>
        /// <returns></returns>
        public abstract bool IsHostile();

        /// <summary>
        /// Some attributes of the creature.
        /// </summary>
        public abstract CreatureAttributes GetAttributes();

        /// <summary>
        /// All the equipped armor that the creature has.
        /// </summary>
        public List<IArmor> Armoring = new List<IArmor>();

        /// <summary>
        /// All of the equipped rings that the creature has.
        /// </summary>
        public List<IRing> Rings = new List<IRing>();

        /// <summary>
        /// All of the equipped amulets that the creature has.
        /// </summary>
        public List<IAmulet> Amulets = new List<IAmulet>();

        /// <summary>
        /// The equipped weapons that the creature has.
        /// </summary>
        public List<IWeapon> Weapons = new List<IWeapon>();

        /// <summary>
        /// All items on a creature that are not equipped go here.
        /// </summary>
        public List<IItem> Inventory = new List<IItem>();

        /// <summary>
        /// It's this creature's turn for action.
        /// </summary>
        public abstract void YourTurn(Encounter encounter);

        /// <summary>
        /// Called whenever the creature receives damage.
        /// </summary>
        /// <param name="damage"></param>
        public abstract void RecieveDamage(Damage damage);

        /// <summary>
        /// Called whenever the creature has a new item equipped.
        /// </summary>
        /// <param name="item"></param>
        public abstract void EquipItem(IItem item);

        /// <summary>
        /// Called whenever the creature has a item unequipped.
        /// </summary>
        /// <param name="item"></param>
        public abstract void OnItemUnequipped(IItem item);

        /// <summary>
        /// Called whenever the creature is healed.
        /// </summary>
        /// <param name="healer">The creature that healed this creature.</param>
        public abstract void OnCreatureHealed(ICreature healer);

        /// <summary>
        /// Called whenever a creature dies.
        /// </summary>
        /// <param name="dead">The creature that is now dead.</param>
        public abstract void OnCreatureDied(ICreature dead);

        /// <summary>
        /// Called whenever this creature deals damage.
        /// </summary>
        public abstract void OnDealDamage();

        /// <summary>
        /// Raised whenever a creature takes damage.
        /// </summary>
        public event EventHandler<Damage> TakeDamageEvent;

        /// <summary>
        /// Raised whenever a creature deals damage to another creature.
        /// </summary>
        public event EventHandler<Damage> DealDamageEvent;

        /// <summary>
        /// Raised whenever a new item is equipped.
        /// </summary>
        public event EventHandler<ICreature> ItemEquipped;

        /// <summary>
        /// Raised whenever a item is unequipped.
        /// </summary>
        public event EventHandler<ICreature> ItemUnequipped;

        /// <summary>
        /// Raised whenever a creature dies.
        /// </summary>
        public event EventHandler<ICreature> CreatureDied;

        /// <summary>
        /// Raised whenever a creature is healed.
        /// </summary>
        public event EventHandler<ICreature> CreatureHealed;

        /// <summary>
        /// Raised whenever a creature is summoned.
        /// <ICreature>The creature that was summoned.</ICreature>
        /// </summary>
        public event EventHandler<ICreature> CreatureSummoned;

        /// <summary>
        /// Determines based on gender if we should be referring to this creature as a him/her/it.
        /// </summary>
        /// <returns></returns>
        public string HimHerIT()
        {
            if (this.GetAttributes().Gender == Gender.Male)
            {
                return "him";
            }
            if (this.GetAttributes().Gender == Gender.Female)
            {
                return "her";
            }
            if (this.GetAttributes().Gender == Gender.Unspecified)
            {
                return "IT";
            }

            return "IT";
        }

        public string HeSheIT()
        {
            if (this.GetAttributes().Gender == Gender.Male)
            {
                return "Him";
            }
            if (this.GetAttributes().Gender == Gender.Female)
            {
                return "She";
            }
            else
            {
                return "It";
            }
        }

        /// <summary>
        /// The default constructor for this class. Must be called on initialization.
        /// </summary>
        public ICreature()
        {
            this.WeightCapacity = WeightCapacityUtil.Calculate(this);
        }

        /// <summary>
        /// Called whenever an encounter ends, so summoned creatures can be un-summoned.
        /// </summary>
        /// <param name="fight"></param>
        public abstract void EncounterEnded(Encounter fight);
    }
}