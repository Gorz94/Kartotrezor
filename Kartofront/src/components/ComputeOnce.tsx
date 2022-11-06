import { IonButton, IonCard, IonLabel, IonTextarea } from "@ionic/react";
import { useState } from "react";
import { Map } from '../model/Map';
import { computeMap } from "../redux/services/mapService";
import { MapComponent } from "./MapComponent";

export const ComputeOnce: React.FC = () => {
    const [map, setMap] = useState<Map>();
    const [error, setError] = useState<string>();
    const [command, setCommand] = useState<string>();

    const callComputeMap = async () => {
        if  ((command?.length ?? 0) > 0) {
            setError('');

            try {
                const result = await computeMap(command ?? '');

                if (result.error) {
                    setError(result.error);
                } else {
                    setMap(result.map);
                }
            } catch (e)
            {
                setError(JSON.stringify(e));
            }
        } else {
            setError('You need at least one command.');
        }
    }

    return (
        <div style={{display: 'flex', alignItems: 'center', justifyContent: 'center', flexDirection: 'column'}}>
            <IonCard>
                <IonTextarea placeholder="Commands ..." autoGrow={true} value={command}
                    onIonChange={e => setCommand(e.detail.value!)} style={{minWidth: '500px'}}/>
                <IonLabel>{error}</IonLabel>
            </IonCard>

            <IonButton onClick={callComputeMap}>
                <IonLabel>Compute</IonLabel>
            </IonButton><br/>

            <MapComponent map={map} />
        </div>
    );
}