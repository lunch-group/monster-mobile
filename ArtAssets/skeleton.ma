//Maya ASCII 2011 scene
//Name: skeleton.ma
//Last modified: Sat, Jan 17, 2015 07:01:33 PM
//Codeset: 1252
requires maya "2011";
currentUnit -l centimeter -a degree -t film;
fileInfo "application" "maya";
fileInfo "product" "Maya 2011";
fileInfo "version" "2011";
fileInfo "cutIdentifier" "201003190014-771504";
fileInfo "osv" "Microsoft Windows 7 Home Premium Edition, 64-bit Windows 7 Service Pack 1 (Build 7601)\n";
createNode joint -n "Reference";
	addAttr -ci true -sn "liw" -ln "lockInfluenceWeights" -min 0 -max 1 -at "bool";
	setAttr ".uoc" yes;
	setAttr ".mnrl" -type "double3" -360 -360 -360 ;
	setAttr ".mxrl" -type "double3" 360 360 360 ;
	setAttr ".jo" -type "double3" -5.5469834661236562e-031 -5.5469834661236571e-031 
		89.999999999999986 ;
	setAttr ".bps" -type "matrix" 2.2204460492503131e-016 1 0 0 -1 2.2204460492503131e-016 0 0
		 0 0 1 0 0 0 0 1;
	setAttr ".radi" 0.53105535243781699;
createNode joint -n "Hips" -p "Reference";
	addAttr -ci true -sn "liw" -ln "lockInfluenceWeights" -min 0 -max 1 -at "bool";
	setAttr ".uoc" yes;
	setAttr ".oc" 1;
	setAttr ".t" -type "double3" 3.1781774386673876 6.1549153299975402e-016 1.8488927466117464e-032 ;
	setAttr ".mnrl" -type "double3" -360 -360 -360 ;
	setAttr ".mxrl" -type "double3" 360 360 360 ;
	setAttr ".jo" -type "double3" 0 0 -89.999999999999986 ;
	setAttr ".bps" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 9.0205620750793969e-017 3.1781774386673876 1.8488927466117464e-032 1;
	setAttr ".radi" 0.5;
createNode joint -n "Chest" -p "Hips";
	addAttr -ci true -sn "liw" -ln "lockInfluenceWeights" -min 0 -max 1 -at "bool";
	setAttr ".uoc" yes;
	setAttr ".oc" 2;
	setAttr ".t" -type "double3" -1.1102230246251563e-016 2.9096975055474488 -3.1554436208840472e-030 ;
	setAttr ".mnrl" -type "double3" -360 -360 -360 ;
	setAttr ".mxrl" -type "double3" 360 360 360 ;
	setAttr ".jo" -type "double3" 0 0 88.723436127375663 ;
	setAttr ".bps" -type "matrix" 0.022278399382095082 0.99975180566027089 0 0 -0.99975180566027089 0.022278399382095082 0 0
		 0 0 1 0 -2.081668171172166e-017 6.0878749442148363 -3.1369546934179298e-030 1;
	setAttr ".radi" 0.5;
createNode joint -n "Neck" -p "Chest";
	addAttr -ci true -sn "liw" -ln "lockInfluenceWeights" -min 0 -max 1 -at "bool";
	setAttr ".uoc" yes;
	setAttr ".oc" 3;
	setAttr ".t" -type "double3" 0.9354424855480743 0.013296487085821871 -3.0486472405246161e-014 ;
	setAttr ".mnrl" -type "double3" -360 -360 -360 ;
	setAttr ".mxrl" -type "double3" 360 360 360 ;
	setAttr ".jo" -type "double3" 0 0 1.2765638726252364 ;
	setAttr ".bps" -type "matrix" -1.5851903123476063e-014 1.0000000000000002 0 0 -1.0000000000000002 -1.5851903123476063e-014 0 0
		 0 0 1 0 0.0075469743190307963 7.0233814826825327 -3.0486472405246161e-014 1;
	setAttr ".radi" 0.50643961242425839;
createNode joint -n "Head" -p "Neck";
	addAttr -ci true -sn "liw" -ln "lockInfluenceWeights" -min 0 -max 1 -at "bool";
	setAttr ".uoc" yes;
	setAttr ".oc" 4;
	setAttr ".t" -type "double3" 2.1750338476573576 -1.2981947116944619e-014 -2.2097156718824007e-014 ;
	setAttr ".mnrl" -type "double3" -360 -360 -360 ;
	setAttr ".mxrl" -type "double3" 360 360 360 ;
	setAttr ".jo" -type "double3" 0 0 -90.000000000000924 ;
	setAttr ".bps" -type "matrix" 1.0000000000000002 -3.5735303605122542e-016 0 0 3.5735303605122542e-016 1.0000000000000002 0 0
		 0 0 1 0 0.0075469743190092996 9.1984153303398912 -5.2583629124070167e-014 1;
	setAttr ".radi" 0.50643961242425839;
createNode joint -n "LeftShoulder" -p "Chest";
	addAttr -ci true -sn "liw" -ln "lockInfluenceWeights" -min 0 -max 1 -at "bool";
	setAttr ".uoc" yes;
	setAttr ".oc" 3;
	setAttr ".t" -type "double3" 0.23282588495338466 -1.7004726061277093 3.5032461608120427e-046 ;
	setAttr ".mnrl" -type "double3" -360 -360 -360 ;
	setAttr ".mxrl" -type "double3" 360 360 360 ;
	setAttr ".jo" -type "double3" 0 0 -178.72343612737561 ;
	setAttr ".bps" -type "matrix" 1.0408340855860843e-015 -1.0000000000000004 0 0 1.0000000000000004 1.0408340855860843e-015 0 0
		 0 0 1 0 1.7052375465034852 6.282759235243808 -3.1369546934179294e-030 1;
	setAttr ".radi" 0.5;
createNode joint -n "LeftHand" -p "LeftShoulder";
	addAttr -ci true -sn "liw" -ln "lockInfluenceWeights" -min 0 -max 1 -at "bool";
	setAttr ".uoc" yes;
	setAttr ".oc" 4;
	setAttr ".t" -type "double3" 1.9764828328220156 -1.908914237715061e-015 -2.3295601558892542e-014 ;
	setAttr ".mnrl" -type "double3" -360 -360 -360 ;
	setAttr ".mxrl" -type "double3" 360 360 360 ;
	setAttr ".bps" -type "matrix" 1.0408340855860843e-015 -1.0000000000000004 0 0 1.0000000000000004 1.0408340855860843e-015 0 0
		 0 0 1 0 1.7052375465034855 4.3062764024217914 -2.3295601558892546e-014 1;
	setAttr ".radi" 0.5;
createNode joint -n "LeftPalm" -p "LeftHand";
	addAttr -ci true -sn "liw" -ln "lockInfluenceWeights" -min 0 -max 1 -at "bool";
	setAttr ".uoc" yes;
	setAttr ".oc" 5;
	setAttr ".t" -type "double3" 0.39673865914150763 -7.0724317194875036e-016 4.8296869975539281e-015 ;
	setAttr ".mnrl" -type "double3" -360 -360 -360 ;
	setAttr ".mxrl" -type "double3" 360 360 360 ;
	setAttr ".jo" -type "double3" 0 0 89.999999999999929 ;
	setAttr ".bps" -type "matrix" 1.0000000000000004 -4.0245584642661984e-016 0 0 4.0245584642661984e-016 1.0000000000000004 0 0
		 0 0 1 0 1.7052375465034852 3.9095377432802838 -1.8465914561338618e-014 1;
	setAttr ".radi" 0.5;
createNode joint -n "RightShoulder" -p "Chest";
	addAttr -ci true -sn "liw" -ln "lockInfluenceWeights" -min 0 -max 1 -at "bool";
	setAttr ".uoc" yes;
	setAttr ".oc" 3;
	setAttr ".t" -type "double3" 0.15684666865509644 1.7091584961905038 4.6934179296515795e-036 ;
	setAttr ".mnrl" -type "double3" -360 -360 -360 ;
	setAttr ".mxrl" -type "double3" 360 360 360 ;
	setAttr ".jo" -type "double3" 179.99999914622634 -1.6528642850524216e-021 1.2765638726241575 ;
	setAttr ".bps" -type "matrix" 2.976785484776201e-015 1.0000000000000002 0 0 1.0000000000000002 -2.9802549317281546e-015 1.4901161316312335e-008 0
		 1.4901161316312338e-008 -4.4409259136440673e-023 -1 0 -1.7052400000000001 6.2827600000000006 -3.1369500000000001e-030 1;
	setAttr ".radi" 0.5;
createNode joint -n "RightHand" -p "RightShoulder";
	addAttr -ci true -sn "liw" -ln "lockInfluenceWeights" -min 0 -max 1 -at "bool";
	setAttr ".uoc" yes;
	setAttr ".oc" 4;
	setAttr ".t" -type "double3" -1.9764800000000005 5.773159728050814e-015 2.3295600086511052e-014 ;
	setAttr ".mnrl" -type "double3" -360 -360 -360 ;
	setAttr ".mxrl" -type "double3" 360 360 360 ;
	setAttr ".bps" -type "matrix" 2.976785484776201e-015 1.0000000000000002 0 0 1.0000000000000002 -2.9802549317281546e-015 1.4901161316312335e-008 0
		 1.4901161316312338e-008 -4.4409259136440673e-023 -1 0 -1.7052400000000001 4.3062799999999992 -2.3295600000484272e-014 1;
	setAttr ".radi" 0.5;
createNode joint -n "RightPalm" -p "RightHand";
	addAttr -ci true -sn "liw" -ln "lockInfluenceWeights" -min 0 -max 1 -at "bool";
	setAttr ".uoc" yes;
	setAttr ".oc" 5;
	setAttr ".t" -type "double3" -0.39673999999999898 1.3322676295501878e-015 -4.8296999809808227e-015 ;
	setAttr ".mnrl" -type "double3" -360 -360 -360 ;
	setAttr ".mxrl" -type "double3" 360 360 360 ;
	setAttr ".jo" -type "double3" -8.5377365326830046e-007 -8.537736532683031e-007 89.999999999999815 ;
	setAttr ".bps" -type "matrix" 1.0000000000000002 3.5041414214731582e-016 1.4901161316312335e-008 0
		 3.5388358909926944e-016 -1.0000000000000002 4.9630837161070977e-023 0 1.4901161316312338e-008 -4.4409259136440673e-023 -1 0
		 -1.7052399999999999 3.9095400000000002 -1.8465899999651114e-014 1;
	setAttr ".radi" 0.5;
createNode joint -n "joint11" -p "Hips";
	addAttr -ci true -sn "liw" -ln "lockInfluenceWeights" -min 0 -max 1 -at "bool";
	setAttr ".uoc" yes;
	setAttr ".oc" 2;
	setAttr ".t" -type "double3" 0.52721044295585195 -0.56006810584912015 1.3066177254900754e-014 ;
	setAttr ".mnrl" -type "double3" -360 -360 -360 ;
	setAttr ".mxrl" -type "double3" 360 360 360 ;
	setAttr ".jo" -type "double3" 0 0 -89.999999999999986 ;
	setAttr ".bps" -type "matrix" 2.2204460492503131e-016 -1 0 0 1 2.2204460492503131e-016 0 0
		 0 0 1 0 0.52721044295585207 2.6181093328182676 1.3066177254900754e-014 1;
	setAttr ".radi" 0.52391627395800644;
createNode joint -n "joint12" -p "joint11";
	addAttr -ci true -sn "liw" -ln "lockInfluenceWeights" -min 0 -max 1 -at "bool";
	setAttr ".uoc" yes;
	setAttr ".oc" 3;
	setAttr ".t" -type "double3" 2.5464251653701049 -2.9621724381115774e-016 2.8250107319520678e-014 ;
	setAttr ".mnrl" -type "double3" -360 -360 -360 ;
	setAttr ".mxrl" -type "double3" 360 360 360 ;
	setAttr ".jo" -type "double3" 89.999999999999972 -89.999999999998366 0 ;
	setAttr ".bps" -type "matrix" 6.3108872417680944e-030 -2.8421709430404007e-014 1 0
		 2.2204460492503131e-016 1 2.8421709430404007e-014 0 -1 2.2204460492503131e-016 0 0
		 0.52721044295585229 0.07168416744816275 4.1316284574421432e-014 1;
	setAttr ".radi" 0.52391627395800644;
createNode joint -n "joint15" -p "|Reference|Hips|joint11|joint12";
	addAttr -ci true -sn "liw" -ln "lockInfluenceWeights" -min 0 -max 1 -at "bool";
	setAttr ".uoc" yes;
	setAttr ".oc" 4;
	setAttr ".t" -type "double3" 0.43446472477961162 2.170486013142181e-014 8.2711615334574162e-015 ;
	setAttr ".mnrl" -type "double3" -360 -360 -360 ;
	setAttr ".mxrl" -type "double3" 360 360 360 ;
	setAttr ".jo" -type "double3" 0 89.999999999999986 0 ;
	setAttr ".bps" -type "matrix" 1 -2.2204460492503762e-016 2.2204460492503131e-016 0
		 2.2204460492503131e-016 1 2.8421709430404007e-014 0 -2.22044604925025e-016 -2.8421709430404007e-014 1 0
		 0.52721044295584407 0.071684167448172104 0.43446472477965292 1;
	setAttr ".radi" 0.5;
createNode joint -n "joint19" -p "Hips";
	addAttr -ci true -sn "liw" -ln "lockInfluenceWeights" -min 0 -max 1 -at "bool";
	setAttr ".uoc" yes;
	setAttr ".oc" 2;
	setAttr ".t" -type "double3" -0.52721000000000007 -0.5600674386673874 1.3066200000000001e-014 ;
	setAttr ".mnrl" -type "double3" -360 -360 -360 ;
	setAttr ".mxrl" -type "double3" 360 360 360 ;
	setAttr ".jo" -type "double3" -180 0 89.999999999999986 ;
	setAttr ".bps" -type "matrix" 2.2204460492503131e-016 1 0 0 1 -2.2204460492503131e-016 -1.2246467991473532e-016 0
		 -1.2246467991473532e-016 2.4651903288156619e-032 -1 0 -0.52720999999999996 2.6181100000000002 1.3066200000000001e-014 1;
	setAttr ".radi" 0.52391627395800644;
createNode joint -n "joint12" -p "joint19";
	addAttr -ci true -sn "liw" -ln "lockInfluenceWeights" -min 0 -max 1 -at "bool";
	setAttr ".uoc" yes;
	setAttr ".oc" 3;
	setAttr ".t" -type "double3" -2.5464258000000002 5.5511151231257827e-016 -2.8250099999999994e-014 ;
	setAttr ".mnrl" -type "double3" -360 -360 -360 ;
	setAttr ".mxrl" -type "double3" 360 360 360 ;
	setAttr ".jo" -type "double3" 89.999999999999986 -89.99999999999838 0 ;
	setAttr ".bps" -type "matrix" -2.3348698237724464e-016 2.8310687127941492e-014 -0.99999999999999978 0
		 -3.4109572753460238e-030 -0.99999999999999978 -2.8255175976710228e-014 0 -1 1.1102230246251568e-016 1.2246467991473532e-016 0
		 -0.52720999999999996 0.071684199999999976 4.1316299999999992e-014 1;
	setAttr ".radi" 0.52391627395800644;
createNode joint -n "joint15" -p "|Reference|Hips|joint19|joint12";
	addAttr -ci true -sn "liw" -ln "lockInfluenceWeights" -min 0 -max 1 -at "bool";
	setAttr ".uoc" yes;
	setAttr ".oc" 4;
	setAttr ".t" -type "double3" -0.43446499999995875 -1.2323475573339238e-014 1.1102230246251565e-016 ;
	setAttr ".mnrl" -type "double3" -360 -360 -360 ;
	setAttr ".mxrl" -type "double3" 360 360 360 ;
	setAttr ".jo" -type "double3" 1.9083328088781303e-014 89.999999999999972 0 ;
	setAttr ".bps" -type "matrix" 1 -1.1102230246250311e-016 -5.6655388976479786e-016 0
		 -3.4109572753460238e-030 -0.99999999999999978 -2.8255175976710228e-014 0 -6.775761922273073e-016 2.8310687127941492e-014 -0.99999999999999978 0
		 -0.52720999999999996 0.071684200000000003 0.43446499999999993 1;
	setAttr ".radi" 0.5;
select -ne :time1;
	setAttr ".o" 1;
	setAttr ".unw" 1;
select -ne :renderPartition;
	setAttr -s 5 ".st";
select -ne :initialShadingGroup;
	setAttr ".ro" yes;
select -ne :initialParticleSE;
	setAttr ".ro" yes;
select -ne :defaultShaderList1;
	setAttr -s 5 ".s";
select -ne :postProcessList1;
	setAttr -s 2 ".p";
select -ne :renderGlobalsList1;
select -ne :hardwareRenderGlobals;
	setAttr ".ctrs" 256;
	setAttr ".btrs" 512;
select -ne :defaultHardwareRenderGlobals;
	setAttr ".fn" -type "string" "im";
	setAttr ".res" -type "string" "ntsc_4d 646 485 1.333";
connectAttr "Reference.s" "Hips.is";
connectAttr "Hips.s" "Chest.is";
connectAttr "Chest.s" "Neck.is";
connectAttr "Neck.s" "Head.is";
connectAttr "Chest.s" "LeftShoulder.is";
connectAttr "LeftShoulder.s" "LeftHand.is";
connectAttr "LeftHand.s" "LeftPalm.is";
connectAttr "Chest.s" "RightShoulder.is";
connectAttr "RightShoulder.s" "RightHand.is";
connectAttr "RightHand.s" "RightPalm.is";
connectAttr "Hips.s" "joint11.is";
connectAttr "joint11.s" "|Reference|Hips|joint11|joint12.is";
connectAttr "|Reference|Hips|joint11|joint12.s" "|Reference|Hips|joint11|joint12|joint15.is"
		;
connectAttr "Hips.s" "joint19.is";
connectAttr "joint19.s" "|Reference|Hips|joint19|joint12.is";
connectAttr "|Reference|Hips|joint19|joint12.s" "|Reference|Hips|joint19|joint12|joint15.is"
		;
// End of skeleton.ma
