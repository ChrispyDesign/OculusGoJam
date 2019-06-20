/////////////////////////////////////////////////////////////////////////////////////////////////////
//
// Audiokinetic Wwise generated include file. Do not edit.
//
/////////////////////////////////////////////////////////////////////////////////////////////////////

#ifndef __WWISE_IDS_H__
#define __WWISE_IDS_H__

#include <AK/SoundEngine/Common/AkTypes.h>

namespace AK
{
    namespace EVENTS
    {
        static const AkUniqueID CALLDRAW = 3190170207U;
        static const AkUniqueID CALLREADY = 3537723274U;
        static const AkUniqueID CUPMOVE = 1014133256U;
        static const AkUniqueID DOOROPEN = 1404805401U;
        static const AkUniqueID ENDBGAUDIOSCENE = 1524259163U;
        static const AkUniqueID ONGUNTRIGGER = 2690234956U;
        static const AkUniqueID STARTAMBIENCESCENE = 1384108873U;
        static const AkUniqueID STARTMUSICSCENE = 2539414260U;
    } // namespace EVENTS

    namespace STATES
    {
        namespace GAMESTATE
        {
            static const AkUniqueID GROUP = 4091656514U;

            namespace STATE
            {
                static const AkUniqueID GAMEOVER = 4158285989U;
                static const AkUniqueID GAMESTARTED = 2893136410U;
                static const AkUniqueID MAIN_MENU = 2005704188U;
                static const AkUniqueID PREDRAW = 1750233550U;
                static const AkUniqueID PREREADY = 1176400293U;
            } // namespace STATE
        } // namespace GAMESTATE

    } // namespace STATES

    namespace SWITCHES
    {
        namespace GUNAMMO
        {
            static const AkUniqueID GROUP = 1454789091U;

            namespace SWITCH
            {
                static const AkUniqueID EMPTY = 3354297748U;
                static const AkUniqueID FULL = 2510516222U;
            } // namespace SWITCH
        } // namespace GUNAMMO

        namespace SHOTREACTION
        {
            static const AkUniqueID GROUP = 2933688180U;

            namespace SWITCH
            {
                static const AkUniqueID FAILURE = 3878839773U;
                static const AkUniqueID NEUTRAL = 670611050U;
                static const AkUniqueID POSITIVE = 1192865152U;
                static const AkUniqueID SUCCESS = 3625060726U;
                static const AkUniqueID UI = 1551306167U;
            } // namespace SWITCH
        } // namespace SHOTREACTION

    } // namespace SWITCHES

    namespace BANKS
    {
        static const AkUniqueID INIT = 1355168291U;
        static const AkUniqueID GAMESOUNDBANK = 460270446U;
    } // namespace BANKS

    namespace BUSSES
    {
        static const AkUniqueID MASTER_AUDIO_BUS = 3803692087U;
    } // namespace BUSSES

    namespace AUDIO_DEVICES
    {
        static const AkUniqueID NO_OUTPUT = 2317455096U;
        static const AkUniqueID SYSTEM = 3859886410U;
    } // namespace AUDIO_DEVICES

}// namespace AK

#endif // __WWISE_IDS_H__
