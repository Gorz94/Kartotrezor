import { Entity } from "./entities/Entity";
import { Level } from "./Enum";

export interface MapSlot {
    x: number;
    y: number;
    level: Level;
    entities: Entity[]
}