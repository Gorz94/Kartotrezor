import { IonCard, IonLabel, IonSegment, IonSegmentButton } from "@ionic/react";
import { useState } from "react";
import { ComputeComplexe } from "../components/ComputeComplexe";
import { ComputeOnce } from "../components/ComputeOnce";

export const MainPage : React.FC = () => {
    const [tab, setTab] = useState('once');

    return (
        <IonCard>
            <IonSegment value='once' onIonChange={e => setTab(e.detail.value!)}>
                <IonSegmentButton value='once'>
                    <IonLabel>Simple</IonLabel>
                </IonSegmentButton>
                <IonSegmentButton value='complexe'>
                    <IonLabel>Complex</IonLabel>
                </IonSegmentButton>
            </IonSegment>

            {
                tab == 'once' ?
                    <ComputeOnce />
                    :
                    <ComputeComplexe />
            }
        </IonCard>  
    );
}