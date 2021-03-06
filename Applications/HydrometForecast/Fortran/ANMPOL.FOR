	INTEGER THISYR,BP,POS,BNK(2,12)
	DIMENSION  CAP(5),TIT(148),DATA(12),IST(2)
	1         ,ICB(7),ELR(12),OR(12),AIR(12)
	1         ,FIR(12), ETR(12),TOT(3),ELT(12),
	1          ICD(7),ELTR(12),HARRY(3),OTR(12),
	1	  CAFH(50),CAFL(50),CELH(50),CELL(50),
	1	  CQH(50),CQL(50),BNKINF(12),POWER(12)
	CHARACTER*8 PROJ(3),PROJ1(3),RESV(3),DAM(3),ELTA(12)
	CHARACTER*8 CAPDA
	CHARACTER*7 AVYR,TOA(3),CAPNO(2),TOTA(3)
	CHARACTER*7 AR(12),IRA(12),HIR(12),ALR(12),ATR(12),ALTR(12)
	CHARACTER*6 OTL(12),ET(12),EL(12),O(12),FI(12),AI(12)
	CHARACTER*25 M(12),ADD
	CHARACTER*4 NIM(50),YEAR,IB,CBTT(2)
	CHARACTER*9 PCODE(7),BNKCD(2)
	CHARACTER*10 STRING
	CHARACTER*12 NAM(50)
	CHARACTER*1 INFL,ANS
C
	COMMON ADD
C
C	OPEN(UNIT=1,FILE='HYDROMET:[MPOLL]MONTHLY.IND',STATUS='OLD',
C	1ORGANIZATION='INDEXED',ACCESS='KEYED',READONLY,
C	2RECORDTYPE='FIXED',SHARED,FORM='UNFORMATTED',RECL=18,
C	3KEY=(1:10:CHARACTER),IOSTAT=IOS,ERR=9999)
C
	INCLUDE 'DRA2:[MPOLL]OPEN.MP/LIST'
	DATA M/'OCTOBER   ','NOVEMBER  ','DECEMBER  ','JANUARY   ',
	1       'FEBRUARY  ','MARCH     ','APRIL     ','MAY       ',
	1       'JUNE      ','JULY      ','AUGUST    ','SEPTEMBER '/
	DATA IS/-1/
      DATA NIM/'THF ','PHL ','CMO ','HGH ','AND ','ARK ','LOW ',
     +         'LUC ','CSC ','DED ','BNK ','GCL ','POT ','UNY ',
     +         'OCH ','PRV ','CRE ','HAY ','CRA ','WIC ','WOD ',
     +         'RIR ','AMF ','GRS ','ISL ','JCK ','MIN ','PAL ',
     +         'CCL ','CCR ','OWY ','MAN ','EMI ','HPD ','AGA ',
     +         'HYA ','MCK ','CLS ','SCO ','BEU ','BUL ','WAR ',
     +         'BUM ','RIM ','CLE ','KEE ','KAC ','CLR ','WAS ',
     +         'WLD '/
	DATA PCODE/'AF       ','AF       ','OM       ','IMA      ',
	1          'IM       ','FB       ','OLK      '/            
	DATA BNKCD/'PS	     ','YS       '/
	DATA CAFH/19.0,91.5,36.1,3010.0,427.0,290.0,170.0,
	1         279.2,654.2,173.0,716.0,5186.4,339.0,26.2,
	1         47.5,159.0,87.9,6.6,56.3,201.0,31.0,
	1         91.0,1673.6,16.2,137.0,860.0,104.0,1240.0,
	1	  11.5,14.0,745.0,12.1,40.0,61.6,5.7,
	1	  17.0,74.8,51.0,54.6,61.0,31.0,193.0,
	1	  35.7,200.0,438.9,159.8,241.0,6.3,12.9,
	1	  78.0/
	DATA CELH/3135.0,4071.5,4243.7,3561.0,4197.0,3217.0,2531.0,
	1	3061.0,4829.0,5335.0,1571.0,1291.0,1047.5,3821.0,
	1	3131.9,3235.8,4848.0,2843.0,4446.0,4338.66,5338.3,
	1	5113.8,4355.1,7211.0,6308.0,6771.0,4246.0,5621.0,
	1	2325.25,2288.0,2671.0,2890.0,2242.0,4528.6,1511.0,
	1	5017.0,1318.1,622.5,304.5,3341.0,2517.0,3407.0,
	1	3427.0,2927.0,2241.0,2518.0,2263.0,3014.00,3515.4,
	1	6208.0/
	DATA CELL/3094.0,3475.0,4185.75,3336.0,3992.0,2964.5,2500.0,
	1	2822.0,4787.5,5202.75,1517.20,1208.0,1022.5,3776.0,
	1	3049.0,3112.0,4822.0,2780.0,4424.0,4259.75,5127.8,
	1	4997.0,4277.0,7135.0,6230.0,6730.0,4192.0,5452.3,
	1	2282.1,2232.4,2367.5,2822.0,2131.5,4471.0,1465.0,
	1	4981.7,1182.0,560.0,229.0,3263.21,2456.58,3327.0,
	1	3389.6,2766.0,2110.0,2425.0,2192.75,2958.50,3480.0,
	1	6140.0/
	DATA CQH/80.0,40.0,60.0,900.0,400.0,650.0,75.0,
	1	900.0,400.0,200.0,600.0,10000.0,200.0,40.0,
	1	30.0,200.0,16.0,19.0,35.0,110.0,75.0,
	1	110.0,1600.0,6.0,150.0,500.0,1600.0,1600.0,
	1	4.0,25.0,800.0,16.0,20.0,15.0,2.5,
	1	5.0,45.0,15.0,40.0,50.0,80.0,110.0,
	1	75.0,100.0,220.0,70.0,85.0,0.0,0.0,
	1	70.0/
C
C
5400	FORMAT(22H |WATER YEAR|PROJECT--,3A8,30X,1H|/14H |  ENDING  | ,
	1      8X,3A8, 5X,25HPACIFIC NORTHWEST REGION ,1H|/1H+,12X,
	1      63(1H_)/23H |  SEPT.30 |RESERVOIR-,3A8,5H|DAM-,3A8, 1H|/
	1      1H+,12X,63(1H_)/5H |   ,I4,26H   |ACTIVE CAPACITY       ,
	1      F10.1,19H  |TOTAL CAPACITY  ,F10.1, 3H  |/1H+, 12X,
	1      63(1H_)/26H |          |ACTIVE CONS. ,F8.1,11H|JOINT USE ,
	1      F10.1,10H|FL.CONT. ,F10.1,2H |/1H+,12X,63(1H_),
	1      /49H |          |   ACTIVE STORAGE  |        | INFLOW,
	1      28H(1000 AF) |         |WATER |/1H+,41X,17(1H_)/
	1      51H |  UNITS   |    END OF MONTH   | OUTFLOW|        |,A7,
	1      19H |ELEVATION|SUPPLY|,/1H+,12X,19(1H_)/ 9H |  1,000,
	1      51H   |     WATER YEAR    | 1000 AF|CURRENT |AVERAGE |,
	1      17H  (MSL)  |OUT-  |/1H+,12X,19(1H_)/15H |ACRE FEET |  ,
	1      I4,6H   |  ,I4,3X,4(1H|,8X),9H |LOOK  |/2H+ ,74(1H_)/
	1      12(2H |,A10,1H|,2(2X,A6,2H |),3(A7,1X,1H|),1X,A8,2H| ,
	1      A6/)2H+ ,74(1H_)/13H |TOTALS    |,
	1      2(9X,1H|),2(A7,2H |),A7,2H |,9X, 1H|,6X, 1H|/2H+ ,
	1      74(1H_)/4(2H |,37A2,1H|/)2H |,74(1H-),1H|/11H |MAXIMUM E
	1      44HLEV.OF SURCHARGE|CAPACITY CURVE NO. AND DATE,21X,1H|/
	1      2H |,8X,F11.1,6X,2H| ,2A7,10X,A8,15X,1H|/2H+ ,74(1H_))
C
5401	FORMAT(2H  ,74X,1H ,/,2H  ,74(1H_),1H )
C
5500	FORMAT(1H1,25X,26HANNUAL WATER SUPPLY REPORT/2H  ,74(1H_),1H )
C
5600	FORMAT(22H |WATER YEAR|PROJECT--,3A8,30X,1H|/14H |  ENDING  | ,
	1      8X,3A8, 5X,25HPACIFIC NORTHWEST REGION ,1H|/1H+,12X,
	1      63(1H_)/23H |  SEPT.30 |RESERVOIR-,3A8,5H|DAM-,3A8, 1H|/
	1      1H+,12X,63(1H_)/5H |   ,I4,26H   |ACTIVE CAPACITY       ,
	1      F10.1,19H  |TOTAL CAPACITY  ,F10.1, 3H  |/1H+, 12X,
	1      63(1H_)/26H |          |ACTIVE CONS. ,F8.1,11H|JOINT USE ,
	1      F10.1,10H|FL.CONT. ,F10.1,2H |/1H+,12X,63(1H_),
	1      /48H |          |   ACTIVE STORAGE  |        |INFLOW,
	1      29H(1000 DAM3)|         |WATER |/1H+,41X,17(1H_)/
	1      51H |  UNITS   |    END OF MONTH   | OUTFLOW|        |,A7,
	1      19H |ELEVATION|SUPPLY|,/1H+,12X,19(1H_)/ 9H |  CUBIC,
	1      51H   |     WATER YEAR    |  1000  |CURRENT |AVERAGE |,
	1      17H  (MSL)  |OUT-  |/1H+,12X,19(1H_)/15H |DEKAMETERS|  ,
	1      I4,3X,1H|,I6,3X,9H|  DAM3  ,2(1H|,8X),18H|(METERS) |LOOK  |
	1      /2H+ ,74(1H_)/12(2H |,A10,1H|,2(A8,2H |),3(A7,2H |),
	1      A8,3H | ,A6/)2H+ ,74(1H_)/13H |TOTALS    |,
	1      2(9X,1H|),2(A7,2H |),A7, 2H |,9X,1H|,6X,1H|/2H+ ,
	1      74(1H_)/4(2H |,37A2,1H|/)2H +,74(1H-),1H+/11H |MAXIMUM E
	1      44HLEV.OF SURCHARGE|CAPACITY CURVE NO. AND DATE,21X,1H|/
	1      2H +,6X,F11.1,10H METERS | ,2A7,10X,A8,15X,1H+/2H+ ,74(1H_))
C
5700	FORMAT(1H1,20X,35HANNUAL WATER SUPPLY REPORT (METRIC)/
	12H +,74(1H-),1H+)
1000	FORMAT(3A8,5X,6A8,A,/,3A8,/,5F6.0,F7.0,2A7,A8,A6,A7,
	14(/37A2))
1010	FORMAT(A3,1X,A2,I2,12A6)
1011	FORMAT(A3,1X,A2,2X,12A6)
C
	DO 67 I=1,50
	NAM(I)=NIM(I)//'        '
	CAFL(I)=0.
	CQL(I)=0.
67	CONTINUE
	CALL IDATE(MON,IDA,IYC)
	IYEAR=IYC+1900
C	THISYR=IYC+1900
C	LASTYR=IYC+1900-1
C	THISYR=1986
C	LASTYR=1985
C
C
507	WRITE(6,1003)
1003	FORMAT(' ENTER WATER YEAR >',$)
	READ(6,1004)THISYR
1004	FORMAT(I4)
	IF(THISYR .LT. 1900 .OR. THISYR .GT. 2000)THEN
		WRITE(6,1005)THISYR
1005	FORMAT(' RENTER WATER YEAR = ',I6)
		GO TO 507
	ELSE IF(IYEAR .NE. THISYR)THEN
C		WRITE(6,1006)THISYR
1006	FORMAT(/' PREPARING ANNUAL REPORT FOR ',I4)	
	END IF
	LASTYR=THISYR-1
C
C
	WRITE(6,1001)
1001	FORMAT(/' Inflow data is entered on MPOLL using the IM code',/,
	1	' which the program reads.  The program ,however,',/,
	1	' can calculate inflow.',/, 
	1	/,' Should I calculate INFLOW ?',$)
1002	FORMAT(A)
	READ(5,1002)INFL
C
C
	IPROJS=50
	ISTART=1
C
C       
	WRITE(6,1013)
1013	FORMAT(/' If you do not want reports for all of the projects,'/,
	1	' the program can select various reports.  If you',/
	1	' want a particular report you must enter the two',/
	1       ' corresponding CBTT codes for that report, because',/
	1	' two projects are summarized on each page.',//
	1	' Should I select particular report(s) ?',$)
	READ(5,1002)ANS
509	IF(ANS .EQ. 'Y' .OR. ANS .EQ. 'y')THEN
500		WRITE(6,1014)(NIM(I)(1:3),NIM(I+1)(1:3),I=1,50,2)
1014	FORMAT(//' The following pairs of CBTT"S go with one report.',/	
	1/,12(1X,A,',',A,'  ------------------  ',A,',',A,/)1X,A,
	1',',A,//,
	1' You must enter one of the above pairs of CBTT codes in exactly'
	1,/' the same order.  The following is an example entry:  BNK,GCL ',/
	1  ' To quit enter EX ',/
	1  ' For another year enter YEAR ',//
	1  '  ENTER CBTT CODES >',$)
		STRING='          '
		READ(5,1015)NS,(STRING(N:N),N=1,NS)
1015	FORMAT(Q,10A1)
C
		IF(STRING(1:4) .EQ. 'YEAR' .OR. STRING(1:4) .EQ. 'year')go to 507
		IF(STRING(1:1) .EQ. ' ')GO TO 5002
		IF(STRING(1:2) .EQ. 'EX' .OR. STRING(1:2) .EQ. 'ex')GO TO 5002
		IF(STRING(1:3) .EQ. 'END')GO TO 5002
		IF(STRING(1:1) .EQ. 'Q')GO TO 5002
C
		STRING(NS+1:NS+1)=','
		BP=1
		DO 70 I=1,2
		CBTT(I)='    '
		POS=INDEX(STRING(BP:),',')+BP-1
		CBTT(I)=STRING(BP:POS-1)
		BP=POS+1
		MATCH=0
		DO 71 K=1,50
		IF(CBTT(I) .EQ. NIM(K))THEN
			MATCH=1
			GO TO 121
			END IF
71		CONTINUE
121		IF(MATCH .EQ. 0)THEN
			WRITE(6,1016)CBTT(I)
1016	FORMAT(' Could not match CBTT code > ', A/)
			GO TO 500
			END IF
		IST(I)=K
70		CONTINUE
		IF(IST(1) .GT. IST(2))THEN
			IPROJS=IST(1)
			ISTART=IST(2)
		ELSE IF(IST(2) .GT. IST(1))THEN
			IPROJS=IST(2)
			ISTART=IST(1)
		END IF
	END IF
C
C
	IF(INFL .EQ. 'N' .OR. INFL .EQ. 'n')GO TO 506
	ENCODE(4,1023,YEAR)THISYR
	DO 132 I=1,2
	ADD='BNK         '//BNKCD(I)//YEAR
505	READ(UNIT=1,KEY=ADD,KEYID=0,IOSTAT=IOS,ERR=9997)ADD,BNK	
	GO TO 504
9997	IF(IOS .EQ. 52)CALL RECLOCK(*505,*5001)								
	IF(IOS .EQ. 36)THEN
		DO 68 N=1,12
		BNK(I,N)=0.
68		CONTINUE
		END IF
504	DO 69 N=1,12
	IF(BNK(I,N) .EQ. 998877.)POWER(N)=0.
69	CONTINUE 
132	CONTINUE
C
C
506	DO 66 LOOP=ISTART,IPROJS
508	READ(2,1000)PROJ,RESV,DAM,IB,PROJ1,CAP,WSEM,CAPNO,CAPDA,F,AVYR,TIT
C
C
	ITEST=IPROJS-ISTART+1
	IF(ITEST .EQ. 2)THEN
		IF(IB .NE. NAM(LOOP))GO TO 508
		END IF
C
C
50	DO 51 I=1,3
	TOT(I)=0.
51	CONTINUE
	L=12
C
	DO 52 I=1,7
	IF(I.EQ.1)ENCODE(4,1023,YEAR)LASTYR
1023	FORMAT(I4)
	IF(I.GT.1)ENCODE(4,1023,YEAR)THISYR
	IF(I.EQ.4)YEAR='9999'
	ADD=NAM(LOOP)//PCODE(I)//YEAR
501	READ(UNIT=1,KEY=ADD,KEYID=0,IOSTAT=IOS,ERR=9998)ADD,DATA
	GO TO 503
9998	IF(IOS.EQ.52)CALL RECLOCK(*501,*5001)
C
C------------------------------------------------------------------
C
	IF(IOS .EQ. 36)THEN
		MISS=1
		DO 44 IZ=1,12
		DATA(IZ)=998877.0
44		CONTINUE
		WRITE(7,1022)ADD
1022		FORMAT(1X,A, ' Data record is missing on MPOLL')
	END IF
C
C------------------------------------------------------------------
C 
503	IF(I.EQ.1)THEN
		DO 56 N=1,12
		ELR(N)=DATA(N)
56		CONTINUE
		IF(IOS .EQ. 36 )GO TO 510
		CALL CHECK(CAFH(LOOP),CAFL(LOOP),DATA)
		CALL CNVRTRA(L,ELR,EL)
510		CONTINUE
	ELSE IF(I.EQ.2)THEN
		DO 57 N=1,12
		ETR(N)=DATA(N)
57		CONTINUE
		IF(IOS .EQ. 36 )GO TO 511
		CALL CHECK(CAFH(LOOP),CAFL(LOOP),DATA)
		CALL CNVRTRA(L,ETR,ET)
511		CONTINUE
	ELSE IF(I.EQ.3)THEN
		DO 58 N=1,12
		OR(N)=DATA(N)
58		CONTINUE
		IF(IOS .EQ. 36 )GO TO 512
		CALL CHECK(CQH(LOOP),CQL(LOOP),DATA)
		CALL CNVRTRA(L,OR,O)
512		CONTINUE
	ELSE IF(I.EQ.4)THEN
		DO 59 N=1,12
		AIR(N)=DATA(N)
59		CONTINUE
		IF(IOS .EQ. 36 )GO TO 513
C		CALL CHECK(CQH(LOOP),CQL(LOOP),DATA)
		CALL CNVRTTA(L,AIR,AI)
513		CONTINUE
	ELSE IF(I.EQ.5)THEN
		DO 60 N=1,12
		IF(INFL(1:1) .EQ. 'N' .OR. INFL .EQ. 'n')THEN
			FIR(N)=DATA(N)
		ELSE IF(INFL(1:1) .EQ. 'Y' .OR. INFL .EQ. 'y')THEN
			IF(N.EQ.1)THEN
				DELTA=ETR(1)-ELR(12)
				IF(ELR(12) .EQ. 998877. )DELTA=998877.
			END IF
			IF(N.GT.1)DELTA=ETR(N)-ETR(N-1)
			IF(NAM(LOOP)(1:4) .EQ. 'GCL ')DELTA=DELTA+BNK(1,N)-BNK(2,N)
			IF(ETR(N) .EQ. 998877. .OR. ETR(N-1) .EQ. 998877.)DELTA=998877.
			FIR(N)=DELTA+OR(N)
			IF(DELTA .EQ. 998877. .OR. OR(N) .EQ. 998877.)FIR(N)=998877.
			IF(FIR(N).LT.0.)FIR(N)=0.
		END IF
C		IF(NAM(LOOP)(1:4) .EQ. 'BNK ')FIR(N)=DELTA+OR(N)
60			CONTINUE
		IF(IOS .EQ. 36 )GO TO 514
		CALL CHECK(CQH(LOOP),CQL(LOOP),DATA)
		CALL CNVRTRA(L,FIR,FI)
514		CONTINUE
	ELSE IF(I.EQ.6)THEN
		DO 61 N=1,12
		IF(DATA(N).GT.8500.AND.DATA(N).NE.998877.)DATA(N)=DATA(N)*.01
		ELTR(N)=DATA(N)
61		CONTINUE
		IF(IOS .EQ. 36 )GO TO 515
		CALL CHECK(CELH(LOOP),CELL(LOOP),DATA)
		CALL CNVRTRA(L,ELTR,ELTA)
515		CONTINUE
	ELSE IF(I.EQ.7)THEN
		DO 62 N=1,12
		OTR(N)=DATA(N)
62		CONTINUE
	END IF
52	CONTINUE
1012	FORMAT(A3,1X,A2,2X,12F6.2)
	TTT=0.
	DO 213 I=1,12
	ELT(I)=ELTR(I)
	TTT=ELTR(I)+TTT
213	CONTINUE
	IF(TTT.NE.0.) GO TO 1021
	DO 215 I=1,12
	ELTA(I)='      '
215	CONTINUE
	KB=1
C
C
C
1021	DO 180 I=1,12
	IF(OTR(I).EQ.1.)OTL(I)='ABV  |'
	IF(OTR(I).EQ.0.)OTL(I)='NOR  |'
	IF(OTR(I).EQ.-1.)OTL(I)='SUB  |'
	IF(OTR(I).EQ.998877.0)THEN
			OTL(I)='NOR  |'
			OTR(I)=0.
			END IF
	IF(ELT(I).EQ.998877.0)THEN
			ELTA(I)='        '
			ELT(I)=0.
			ELTR(I)=0.
			END IF
	IF(ELR(I).EQ.998877.0)THEN
			EL(I)='      '     
			ELR(I)=0.
			END IF
	IF(ETR(I).EQ.998877.0)THEN
			ET(I)='      '
			ETR(I)=0.
			END IF
	IF(OR(I).EQ.998877.0)THEN
			O(I)='      '
			OR(I)=0.
			END IF
	IF(AIR(I).EQ.998877.0)THEN
			AI(I)='      '
			AIR(I)=0.
			END IF
	IF(FIR(I).EQ.998877.0)THEN
			FI(I)='      '
			FIR(I)=0.
			AI(I)='      '
			AIR(I)=0.
			END IF
180	CONTINUE
	DO 100 I=1,12
	TOT(1)=OR(I)+TOT(1)
	TOT(2)=FIR(I)+TOT(2)
	TOT(3)=AIR(I)+TOT(3)
100	CONTINUE
C
	DO 110 I=1,3
110	CONTINUE
	L=3
C
	CALL CNVRTOT(L,TOT,TOA)
C
	IYR=1900+IYR
	IYR1=1900+IYR1
C
	DO 111 I=1,3
	IF(TOT(I).EQ.0.) TOA(I)='       '
111	CONTINUE
C
	IS=-IS
	IF(IS.GT.0) WRITE(3,5500)
	IF(IS.GT.0) GO TO 5501
	WRITE(3,5401)
C
5501	WRITE(3,5400)PROJ,PROJ1,RESV,DAM,THISYR,CAP(2),CAP(1),CAP(3),
	1    CAP(4),CAP(5),AVYR,LASTYR,THISYR,(M(I),EL(I),ET(I),O(I),FI(I),
	1    AI(I),ELTA(I),OTL(I),I=1,12),TOA,(TIT(I),I=1,148),
	1    WSEM,CAPNO,CAPDA
C
	IF(IS .GT.0) WRITE(4,5700)
C
	DO 120 I=1,12
	ELR(I)=ELR(I)*1.233489
	ETR(I)=ETR(I)*1.233489
	OR(I)=OR(I)*1.233489
	AIR(I)=AIR(I)*1.233489
	FIR(I)=FIR(I)*1.233489
	ELTR(I)=ELTR(I)*.3048006
120	CONTINUE
C
	DO 130 I=1,5
	CAP(I)=CAP(I)*1.233489
130	CONTINUE
	WSEM=WSEM*.3048006
C
	L=12
	CALL CNVRTOT(L,ELR,ALR)
	CALL CNVRTOT(L,ETR,ATR)
	CALL CNVRTOT(L,OR,AR)
	CALL CNVRTOT(L,AIR,HIR)
	CALL CNVRTOT(L,FIR,IRA)
	CALL CNVRTOT(L,ELTR,ALTR)
	IF(KB.NE.1) GO TO 131
	DO 214 I=1,12
	ALTR(I)='       '
214	CONTINUE
131	DO 140 I=1,3
	TOT(I)=TOT(I)*1.233489
140	CONTINUE
	DO 1195 I=1,3
	HARRY(I)=TOT(I)
1195	CONTINUE
C
	L=3
	CALL CNVRTOT(L,TOT,TOTA)
C
	DO 160 I=1,3
	IF(HARRY(I).EQ.0.) TOTA(I)='       '
160	CONTINUE
C
	DO 170 I=1,12
	IF(EL(I).EQ.'      ') ALR(I)='       '
	IF(ET(I).EQ.'      ') ATR(I)='       '
	IF(O(I).EQ.'      ')  AR(I)='       '
 	IF(AI(I).EQ.'      ') HIR(I)='       '
	IF(FI(I).EQ.'      ') IRA(I)='       '
	IF(ELTA(I).EQ.'        ')ALTR(I)='       '
170	CONTINUE
	IF(IS.GT.0) GO TO 5601
	WRITE(4,5401)
C
5601	WRITE(4,5600)PROJ,PROJ1,RESV,DAM,THISYR,CAP(2),CAP(1),CAP(3),
	1CAP(4),CAP(5),AVYR,LASTYR,THISYR,(M(I),ALR(I),ATR(I),AR(I),
	1IRA(I),HIR(I),ALTR(I),OTL(I),I=1,12),TOTA,(TIT(I),I=1,148),
	1WSEM,CAPNO,CAPDA
!	IF(LOOP.EQ.2)GO TO 5001
66	CONTINUE
	IF(MISS .EQ. 1)WRITE(6,1018)
1018	FORMAT(' Check ANMPOL.LOG some MPOLL data records are missing.')
	IF(ANS .EQ. 'Y')THEN
		REWIND 2
		GO TO 509
		END IF
	GO TO 5001
5002	CONTINUE
	WRITE(6,1019)
1019	FORMAT(/,' Output files are ENGLISH.REP and METRIC.REP',/,
	1        ' ENGLISH.REP is the only file setup for the laser printer')
1017	FORMAT(/,' Output file is ENGLISH.REP',//,
	1' For the Laser printer I will use:'/,
	1'   12 chrs. per inch',/
	1'    6 vertical lines per inch',/
	1'    0 top margin',/
	1'   66 bottom margin',/
	1'   12 spaces left margin',/
	1'  132 spaces right margin')
	GO TO 5001
9999    WRITE(6,1029)IOS
1029	FORMAT(1X,'ERROR ON OPEN -- IOS=',I2)
5001	STOP
	END
	SUBROUTINE CNVRTAR(L,RDATA,ADATA)
	COMMON ADD
	DIMENSION ADATA(12)
	CHARACTER*25 ADD
	CHARACTER*6 RDATA(12)
	DO 15 I=1,L
	READ(RDATA(I),10,ERR=9999,IOSTAT=IOS)ADATA(I)
10	FORMAT(F6.0)
15	CONTINUE
	GO TO 501
9999	WRITE(6,1000)ADD,IOS,(ADATA(I),RDATA(I),I=1,12)
1000	FORMAT(1X,'ERROR ON INTERNAL READ CNVRTAR',/,1X,'ADD=',A,
     +  2X,'IOS=',I2,12(/,'ADATA=',F9.2,2X,'RDATA=',A))
	STOP
501	RETURN
	END
	SUBROUTINE CNVRTRA(L,RDATA,ADATA)
	COMMON ADD
	CHARACTER*25 ADD
	CHARACTER*(*) ADATA(12)
	DIMENSION RDATA(12)
	IP=2
1	DO 15 I=1,L
	LE=LEN(ADATA(I))
	IF(RDATA(I).GT.9999.)THEN
C		WRITE(6,1001)ADD,RDATA(I)
C1001	FORMAT('%CNVRTRA  -- DATA EXCEEDS 9999  ',A,'=',F9.2)
		IF(RDATA(I).EQ.998877.)GO TO 25
		IF(ADD(1:4).EQ.'GCL ')GO TO 20
25		ADATA(I)='        '
		GO TO 15
20		END IF
	WRITE(ADATA(I),10,ERR=9999,IOSTAT=IOS)RDATA(I)
10	FORMAT(F<LE>.<IP>)
15	CONTINUE
	GO TO 501
9999	IF(IP.EQ.0)GO TO 9998
	IP=IP-1
	GO TO 1
9998	WRITE(6,1000)ADD,IOS,(ADATA(I),RDATA(I),I=1,12)
1000	FORMAT(1X,'ERROR ON INTERNAL WRITE CNVRTRA',/,1X,'ADD=',A,
     +  2X,'IOS=',I2,12(/,'ADATA=',A,2X,'RDATA=',F9.2))
	STOP
501	RETURN
	END
	SUBROUTINE CNVRTTA(L,RDATA,ADATA)
	COMMON ADD
	CHARACTER*25 ADD
	CHARACTER*(*) ADATA(12)
	DIMENSION RDATA(12)
	IP=1
1	DO 15 I=1,L
	LE=LEN(ADATA(I))
	IF(RDATA(I).GT.9999.)THEN
C		WRITE(6,1001)ADD,RDATA(I)
C1001	FORMAT('%CNVRTRA  -- DATA EXCEEDS 9999  ',A,'=',F9.2)
		IF(ADD(1:4).EQ.'GCL ')GO TO 20
		ADATA(I)='        '
		GO TO 15
20		END IF
	WRITE(ADATA(I),10,ERR=9999,IOSTAT=IOS)RDATA(I)
10	FORMAT(F<LE>.<IP>)
15	CONTINUE
	GO TO 501
9999	IF(IP.EQ.0)GO TO 9998
	IP=IP-1
	GO TO 1
9998	WRITE(6,1000)ADD,IOS,(ADATA(I),RDATA(I),I=1,12)
1000	FORMAT(1X,'ERROR ON INTERNAL WRITE CNVRTTA',/,1X,'ADD=',A,
     +  2X,'IOS=',I2,12(/,'ADATA=',A,2X,'RDATA=',F9.2))
	STOP
501	RETURN
	END
	SUBROUTINE CNVRTOT(L,RDATA,ADATA)
	COMMON ADD
	CHARACTER*25 ADD
	CHARACTER*(*) ADATA(12)
	DIMENSION RDATA(12)
	IP=1
1	DO 15 I=1,L
	LE=LEN(ADATA(I))
	IF(RDATA(I).EQ.99999.)THEN
		ADATA(I)='       '
		GO TO 15
		END IF
	WRITE(ADATA(I),10,ERR=9999,IOSTAT=IOS)RDATA(I)
10	FORMAT(F<LE>.<IP>)
15	CONTINUE
	GO TO 501
9999	IF(IP.EQ.0)GO TO 9998
	IP=0
	GO TO 1
9998	WRITE(6,1000)ADD,IOS,(ADATA(I),RDATA(I),I=1,12)
1000	FORMAT(1X,'ERROR ON INTERNAL WRITE CNVRTOT',/,1X,'ADD=',A,
     +  2X,'IOS=',I2,12(/,'ADATA=',A,2X,'RDATA=',F9.2))
	STOP
501	RETURN
	END
	SUBROUTINE CHECK (HI,HL,RDATA)
	COMMON ADD
	DIMENSION RDATA(12)
	CHARACTER*25 ADD
	CHARACTER*3 MON(12)
	DATA MON/'OCT','NOV','DEC','JAN','FEB','MAR','APR','MAY','JUN',
	1	'JUL','AUG','SEP'/
	DO 10 I=1,12
	IF(RDATA(I).GT.HI.OR.RDATA(I).LT.HL)THEN
		IF(RDATA(I) .NE. 998877.)WRITE(6,1)ADD,RDATA(I),MON(I),HI,HL
		IF(RDATA(I) .EQ. 998877.)WRITE(6,2)ADD,RDATA(I),MON(I)
		GO TO 20
		END IF
2	FORMAT(1X,'Missing data for' 1X,A,1X,F9.2,1X,A)
1	FORMAT(1X,'Check data for  ',1X,A,1X,F9.2,1X,A,' LIMITS',
	1	F8.2,F8.2)
10	CONTINUE
20	RETURN
	END
