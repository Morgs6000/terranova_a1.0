using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType {
    // Classic | Survival Test | 0.24_SURVIVAL_TEST
    arrow,

    // Indev | 0.31 | 20091231-2
    leather_cap,
    leather_tunic,
    leather_pants,
    leather_boots,
    //studded_helmet,
    //studded_chestplate,
    //studded_leggings,
    //studded_boots,
    chain_helmet,
    chain_chestplate,
    chain_leggings,
    chain_boots,    
    iron_helmet,
    iron_chestplate,
    iron_leggings,
    iron_boots,
    //quiver,
    apple,
    iron_shovel,
    iron_sword,

    // Indev | 0.31 | 20100110
    flint_and_steel,
    iron_axe,
    iron_pickaxe,
    bow,

    // Indev | 0.31 | 20100128
    coal,
    diamond,
    gold_ingot,
    iron_ingot,
    wooden_sword,
    wooden_axe,
    wooden_pickaxe,
    wooden_shovel,
    stone_sword,
    stone_axe,
    stone_pickaxe,
    stone_shovel,
    diamond_sword,
    diamond_axe,
    diamond_pickaxe,
    diamond_shovel,

    // Indev | 0.31 | 20100130
    golden_sword,
    golden_axe,
    golden_pickaxe,
    golden_shovel,
    bowl,
    mushrrom_stew,
    gunpowder,
    //string, // COLOCAR OUTRO NOME
    feather,

    // Indev | 20100206
    bread,
    wooden_hoe,
    stone_hoe,
    iron_hoe,
    diamond_hoe,
    golden_hoe,
    wheat_seeds,
    wheat,

    // Indev | 20100212-1
    golden_helmet,
    golden_chestplate,
    golden_leggings,
    golden_boots,
    diamond_helmet,
    diamond_chestplate,
    diamond_leggings,
    diamond_boots,
        // Studded Armor foi removido entre Indev 0.31 20100204-1 e Indev 20100206.

    // Indev | 20100219
    flint,
    raw_porkchop,
    cooked_porkchop,

    // Infdev | 20100227-1
    golden_apple,

    // Infdev | 20100615
    bucket,
    water_bucket,
    lava_bucket,

    // Infdev | 20100625-2 (Seecret Friday 2)
    saddle,

    // Alpha | 1.0 | 1.0.5
    snowball,

    // Alpha | 1.0 | 1.0.8
    leather,
    milk_bucket,

    // Alpha | 1.0 | 1.0.11 (Seecret Friday 6)
    paper,
    book,
    clay_ball,
    brick,
    slimeball,

    // Alpha | 1.0 | 1.0.14 (Seecret Friday 7)
    egg,
    // Music Disc 13
    // Music Disc cat
    minecart_with_furnace,
    minecart_with_chest,

    // Alpha | 1.1 | 1.1.0 (Seecret Friday 9)
    compass,

    // Alpha | 1.1 | 1.1.1 (Seecret Saturday 1)
    fishing_rod,

    // Alpha | 1.2 Halloween Update | 1.2.0
    clock,
    glowstone_dust,
    cooked_cod,
    raw_cod,

    // Beta | 1.2
    bone,
    bone_meal,
    lapis_lazuli,
    cocoa_beans,
    ink_sac,
    charcoal,
    orange_dye,
    magenta_dye,
    light_blue_dye,
    yellow_dye,
    lime_dye,
    pink_dye,
    gray_dye,
    light_gray_dye,
    cyan_dye,
    purple_dye,
    green_dye,
    red_dye,
    sugar,

    // Beta | 1.4
    cookie,

    // Beta | 1.6
    map,

    // Beta | 1.7
    shears,

    // Beta | 1.8 Adventure Update (Part 1)
    raw_chicken,
    cooked_chicken,
    raw_beef,
    steak,
    ender_pearl,
    melon_seeds,
    melon_slice,
    pumpkin_seeds,
    rotten_flesh,

    // 1.0 Adventure Update (Part 2) | 1.0.0 | Beta 1.9 Prerelease
    blaze_rod,
    ghast_tear,
    gold_nugget,
    nether_wart,

    // 1.0 Adventure Update (Part 2) | 1.0.0 | Beta 1.9 Prerelease 2
    blaze_powder,
    fermented_spider_eye,
    glass_bottle,
    magma_cream,
    // Music Disc blocks
    // Musci Disc chirp
    // Music Disc far
    // Music Disc mall
    // Music Disc mellohi
    // Music Disc stal
    // Music Disc strad
    // Music Disc ward
    // Music Disc 11
    spider_eye,

    // 1.0 Adventure Update (Part 2) | 1.0.0 | Beta 1.9 Prerelease 3
    eye_of_ender,

    // 1.0 Adventure Update (Part 2) | 1.0.0 | Beta 1.9 Prerelease 4
    glistering_melon_slice,
    //ghast_tear,
    // Potion
    // Splash Potions

    // 1.1 | 1.1 | 11w49a
    // Spawn Eggs

    // 1.2 | 1.2.1 | 12w04a
    bottle_o_enchanting,
    fire_charge,
    ocelot_spawn_egg,

    // 1.3 | 1.3.1 | 12w17a
    book_and_quill,
    written_book,

    // 1.3 | 1.3.1 | 12w21a
    emerald,
    enchanted_golden_apple,

    // 1.4 Pretty Scary Update | 1.4.2 | 12w32a
    // Potion of Night Vision
    // Splash Potion of Night Vision

    // 1.4 Pretty Scary Update | 1.4.2 | 12w34a
    carrot,
    golden_carrot,
    potato,
    baked_potato,
    poisonous_potato,
    // Potion of Invisibility
    // Splash Potion of Invisibility

    // 1.4 Pretty Scary Update | 1.4.2 | 12w36a
    carront_on_a_stick,
    nether_star,

    // 1.4 Pretty Scary Update | 1.4.2 | 12w37a
    pumpkin_pie,

    // 1.4 Pretty Scary Update | 1.4.4 | 1.4.3
    // Music Disc wait

    // 1.4 Pretty Scary Update | 1.4.6 | 12w49a
    // Enchanted Book
    // Firework Rocket
    // Firework Star
}