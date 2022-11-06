import { Direction } from "../Enum";

export interface Adventurer {
    direction: Direction;
    name: string;
    treasures: number;
}