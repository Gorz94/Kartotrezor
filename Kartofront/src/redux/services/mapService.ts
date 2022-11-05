import axios from 'axios';
import { KARTOTREZOR_URL } from '../../config/config';
import { Map } from '../../model/Map';

export const initMap = async (command: string) : Promise<{success: boolean, id: string, error: string}> => {
    return (await axios.post(KARTOTREZOR_URL + '/map/init', {
        command: split(command)
    })).data;
}

export const contiuneMap = async (id: string) : Promise<{success: boolean, finished: boolean, map: Map, error: string}> => {
    return (await axios.post(KARTOTREZOR_URL + '/map/continue', { id })).data;
}

export const computeMap = async (command: string) : Promise<{map: Map, error: string}> => {
    return (await axios.post(KARTOTREZOR_URL + '/map/compute', {
        command: split(command)
    })).data;
}

const split = (command: string) : string[] => {
    return command.split('\r\n');
}