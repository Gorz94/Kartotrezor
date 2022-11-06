import { TreasureComponent } from "../components/TreasureComponent";
import { AdvComponent } from "../components/AdvComponent";
import { Entity } from "../model/entities/Entity";
import { Treasure } from "../model/entities/Treasure";
import { Adventurer } from "../model/entities/Adventurer";

export const createEntity = (entity: Entity) => {
    switch (entity.name) {
        case "Treasure": return createTreasure(entity);
        case "Adventurer": return createAdventurer(entity);
        default: throw `Entity ${entity.name} is not supported yes.`;
    }
}

const createTreasure = (entity: Entity) => {
    const t = entity.entity as Treasure;

    return <TreasureComponent count={t.count}/>
}

const createAdventurer = (entity: Entity) => {
    const a = entity.entity as Adventurer;
    return <AdvComponent name={a.name} direction={a.direction} treasures={a.treasures} />
}