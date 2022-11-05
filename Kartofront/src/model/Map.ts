import { MapSlot } from "./MapSlot";

export interface Map {
    width: number;
    height: number;
    slots: MapSlot[]   
}