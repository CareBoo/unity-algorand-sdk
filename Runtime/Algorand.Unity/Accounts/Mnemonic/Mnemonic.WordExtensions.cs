using System;
using System.Collections.Generic;
using Unity.Mathematics;

namespace Algorand.Unity
{
    public partial struct Mnemonic
    {
        internal const string shortWords =
            "aban,abil,able,abou,abov,abse,abso,abst,absu,abus,acce,acci,acco,accu,achi,acid,acou,acqu,acro,act,acti,acto,actr,actu,adap,add,addi,addr,adju,admi,adul,adva,advi,aero,affa,affo,afra,agai,age,agen,agre,ahea,aim,air,airp,aisl,alar,albu,alco,aler,alie,all,alle,allo,almo,alon,alph,alre,also,alte,alwa,amat,amaz,amon,amou,amus,anal,anch,anci,ange,angl,angr,anim,ankl,anno,annu,anot,answ,ante,anti,anxi,any,apar,apol,appe,appl,appr,apri,arch,arct,area,aren,argu,arm,arme,armo,army,arou,arra,arre,arri,arro,art,arte,arti,artw,ask,aspe,assa,asse,assi,assu,asth,athl,atom,atta,atte,atti,attr,auct,audi,augu,aunt,auth,auto,autu,aver,avoc,avoi,awak,awar,away,awes,awfu,awkw,axis,baby,bach,baco,badg,bag,bala,balc,ball,bamb,bana,bann,bar,bare,barg,barr,base,basi,bask,batt,beac,bean,beau,beca,beco,beef,befo,begi,beha,behi,beli,belo,belt,benc,bene,best,betr,bett,betw,beyo,bicy,bid,bike,bind,biol,bird,birt,bitt,blac,blad,blam,blan,blas,blea,bles,blin,bloo,blos,blou,blue,blur,blus,boar,boat,body,boil,bomb,bone,bonu,book,boos,bord,bori,borr,boss,bott,boun,box,boy,brac,brai,bran,bras,brav,brea,bree,bric,brid,brie,brig,brin,bris,broc,brok,bron,broo,brot,brow,brus,bubb,budd,budg,buff,buil,bulb,bulk,bull,bund,bunk,burd,burg,burs,bus,busi,busy,butt,buye,buzz,cabb,cabi,cabl,cact,cage,cake,call,calm,came,camp,can,cana,canc,cand,cann,cano,canv,cany,capa,capi,capt,car,carb,card,carg,carp,carr,cart,case,cash,casi,cast,casu,cat,cata,catc,cate,catt,caug,caus,caut,cave,ceil,cele,ceme,cens,cent,cere,cert,chai,chal,cham,chan,chao,chap,char,chas,chat,chea,chec,chee,chef,cher,ches,chic,chie,chil,chim,choi,choo,chro,chuc,chun,chur,ciga,cinn,circ,citi,city,civi,clai,clap,clar,claw,clay,clea,cler,clev,clic,clie,clif,clim,clin,clip,cloc,clog,clos,clot,clou,clow,club,clum,clus,clut,coac,coas,coco,code,coff,coil,coin,coll,colo,colu,comb,come,comf,comi,comm,comp,conc,cond,conf,cong,conn,cons,cont,conv,cook,cool,copp,copy,cora,core,corn,corr,cost,cott,couc,coun,coup,cour,cous,cove,coyo,crac,crad,craf,cram,cran,cras,crat,craw,craz,crea,cred,cree,crew,cric,crim,cris,crit,crop,cros,crou,crow,cruc,crue,crui,crum,crun,crus,cry,crys,cube,cult,cup,cupb,curi,curr,curt,curv,cush,cust,cute,cycl,dad,dama,damp,danc,dang,dari,dash,daug,dawn,day,deal,deba,debr,deca,dece,deci,decl,deco,decr,deer,defe,defi,defy,degr,dela,deli,dema,demi,deni,dent,deny,depa,depe,depo,dept,depu,deri,desc,dese,desi,desk,desp,dest,deta,dete,deve,devi,devo,diag,dial,diam,diar,dice,dies,diet,diff,digi,dign,dile,dinn,dino,dire,dirt,disa,disc,dise,dish,dism,diso,disp,dist,dive,divi,divo,dizz,doct,docu,dog,doll,dolp,doma,dona,donk,dono,door,dose,doub,dove,draf,drag,dram,dras,draw,drea,dres,drif,dril,drin,drip,driv,drop,drum,dry,duck,dumb,dune,duri,dust,dutc,duty,dwar,dyna,eage,eagl,earl,earn,eart,easi,east,easy,echo,ecol,econ,edge,edit,educ,effo,egg,eigh,eith,elbo,elde,elec,eleg,elem,elep,elev,elit,else,emba,embo,embr,emer,emot,empl,empo,empt,enab,enac,end,endl,endo,enem,ener,enfo,enga,engi,enha,enjo,enli,enou,enri,enro,ensu,ente,enti,entr,enve,epis,equa,equi,era,eras,erod,eros,erro,erup,esca,essa,esse,esta,eter,ethi,evid,evil,evok,evol,exac,exam,exce,exch,exci,excl,excu,exec,exer,exha,exhi,exil,exis,exit,exot,expa,expe,expi,expl,expo,expr,exte,extr,eye,eyeb,fabr,face,facu,fade,fain,fait,fall,fals,fame,fami,famo,fan,fanc,fant,farm,fash,fat,fata,fath,fati,faul,favo,feat,febr,fede,fee,feed,feel,fema,fenc,fest,fetc,feve,few,fibe,fict,fiel,figu,file,film,filt,fina,find,fine,fing,fini,fire,firm,firs,fisc,fish,fit,fitn,fix,flag,flam,flas,flat,flav,flee,flig,flip,floa,floc,floo,flow,flui,flus,fly,foam,focu,fog,foil,fold,foll,food,foot,forc,fore,forg,fork,fort,foru,forw,foss,fost,foun,fox,frag,fram,freq,fres,frie,frin,frog,fron,fros,frow,froz,frui,fuel,fun,funn,furn,fury,futu,gadg,gain,gala,gall,game,gap,gara,garb,gard,garl,garm,gas,gasp,gate,gath,gaug,gaze,gene,geni,genr,gent,genu,gest,ghos,gian,gift,gigg,ging,gira,girl,give,glad,glan,glar,glas,glid,glim,glob,gloo,glor,glov,glow,glue,goat,godd,gold,good,goos,gori,gosp,goss,gove,gown,grab,grac,grai,gran,grap,gras,grav,grea,gree,grid,grie,grit,groc,grou,grow,grun,guar,gues,guid,guil,guit,gun,gym,habi,hair,half,hamm,hams,hand,happ,harb,hard,hars,harv,hat,have,hawk,haza,head,heal,hear,heav,hedg,heig,hell,helm,help,hen,hero,hidd,high,hill,hint,hip,hire,hist,hobb,hock,hold,hole,holi,holl,home,hone,hood,hope,horn,horr,hors,hosp,host,hote,hour,hove,hub,huge,huma,humb,humo,hund,hung,hunt,hurd,hurr,hurt,husb,hybr,ice,icon,idea,iden,idle,igno,ill,ille,illn,imag,imit,imme,immu,impa,impo,impr,impu,inch,incl,inco,incr,inde,indi,indo,indu,infa,infl,info,inha,inhe,init,inje,inju,inma,inne,inno,inpu,inqu,insa,inse,insi,insp,inst,inta,inte,into,inve,invi,invo,iron,isla,isol,issu,item,ivor,jack,jagu,jar,jazz,jeal,jean,jell,jewe,job,join,joke,jour,joy,judg,juic,jump,jung,juni,junk,just,kang,keen,keep,ketc,key,kick,kid,kidn,kind,king,kiss,kit,kitc,kite,kitt,kiwi,knee,knif,knoc,know,lab,labe,labo,ladd,lady,lake,lamp,lang,lapt,larg,late,lati,laug,laun,lava,law,lawn,laws,laye,lazy,lead,leaf,lear,leav,lect,left,leg,lega,lege,leis,lemo,lend,leng,lens,leop,less,lett,leve,liar,libe,libr,lice,life,lift,ligh,like,limb,limi,link,lion,liqu,list,litt,live,liza,load,loan,lobs,loca,lock,logi,lone,long,loop,lott,loud,loun,love,loya,luck,lugg,lumb,luna,lunc,luxu,lyri,mach,mad,magi,magn,maid,mail,main,majo,make,mamm,man,mana,mand,mang,mans,manu,mapl,marb,marc,marg,mari,mark,marr,mask,mass,mast,matc,mate,math,matr,matt,maxi,maze,mead,mean,meas,meat,mech,meda,medi,melo,melt,memb,memo,ment,menu,merc,merg,meri,merr,mesh,mess,meta,meth,midd,midn,milk,mill,mimi,mind,mini,mino,minu,mira,mirr,mise,miss,mist,mix,mixe,mixt,mobi,mode,modi,mom,mome,moni,monk,mons,mont,moon,mora,more,morn,mosq,moth,moti,moto,moun,mous,move,movi,much,muff,mule,mult,musc,muse,mush,musi,must,mutu,myse,myst,myth,naiv,name,napk,narr,nast,nati,natu,near,neck,need,nega,negl,neit,neph,nerv,nest,net,netw,neut,neve,news,next,nice,nigh,nobl,nois,nomi,nood,norm,nort,nose,nota,note,noth,noti,nove,now,nucl,numb,nurs,nut,oak,obey,obje,obli,obsc,obse,obta,obvi,occu,ocea,octo,odor,off,offe,offi,ofte,oil,okay,old,oliv,olym,omit,once,one,onio,onli,only,open,oper,opin,oppo,opti,oran,orbi,orch,orde,ordi,orga,orie,orig,orph,ostr,othe,outd,oute,outp,outs,oval,oven,over,own,owne,oxyg,oyst,ozon,pact,padd,page,pair,pala,palm,pand,pane,pani,pant,pape,para,pare,park,parr,part,pass,patc,path,pati,patr,patt,paus,pave,paym,peac,pean,pear,peas,peli,pen,pena,penc,peop,pepp,perf,perm,pers,pet,phon,phot,phra,phys,pian,picn,pict,piec,pig,pige,pill,pilo,pink,pion,pipe,pist,pitc,pizz,plac,plan,plas,plat,play,plea,pled,pluc,plug,plun,poem,poet,poin,pola,pole,poli,pond,pony,pool,popu,port,posi,poss,post,pota,pott,pove,powd,powe,prac,prai,pred,pref,prep,pres,pret,prev,pric,prid,prim,prin,prio,pris,priv,priz,prob,proc,prod,prof,prog,proj,prom,proo,prop,pros,prot,prou,prov,publ,pudd,pull,pulp,puls,pump,punc,pupi,pupp,purc,puri,purp,purs,push,put,puzz,pyra,qual,quan,quar,ques,quic,quit,quiz,quot,rabb,racc,race,rack,rada,radi,rail,rain,rais,rall,ramp,ranc,rand,rang,rapi,rare,rate,rath,rave,raw,razo,read,real,reas,rebe,rebu,reca,rece,reci,reco,recy,redu,refl,refo,refu,regi,regr,regu,reje,rela,rele,reli,rely,rema,reme,remi,remo,rend,rene,rent,reop,repa,repe,repl,repo,requ,resc,rese,resi,reso,resp,resu,reti,retr,retu,reun,reve,revi,rewa,rhyt,rib,ribb,rice,rich,ride,ridg,rifl,righ,rigi,ring,riot,ripp,risk,ritu,riva,rive,road,roas,robo,robu,rock,roma,roof,rook,room,rose,rota,roug,roun,rout,roya,rubb,rude,rug,rule,run,runw,rura,sad,sadd,sadn,safe,sail,sala,salm,salo,salt,salu,same,samp,sand,sati,sato,sauc,saus,save,say,scal,scan,scar,scat,scen,sche,scho,scie,scis,scor,scou,scra,scre,scri,scru,sea,sear,seas,seat,seco,secr,sect,secu,seed,seek,segm,sele,sell,semi,seni,sens,sent,seri,serv,sess,sett,setu,seve,shad,shaf,shal,shar,shed,shel,sher,shie,shif,shin,ship,shiv,shoc,shoe,shoo,shop,shor,shou,shov,shri,shru,shuf,shy,sibl,sick,side,sieg,sigh,sign,sile,silk,sill,silv,simi,simp,sinc,sing,sire,sist,situ,six,size,skat,sket,ski,skil,skin,skir,skul,slab,slam,slee,slen,slic,slid,slig,slim,slog,slot,slow,slus,smal,smar,smil,smok,smoo,snac,snak,snap,snif,snow,soap,socc,soci,sock,soda,soft,sola,sold,soli,solu,solv,some,song,soon,sorr,sort,soul,soun,soup,sour,sout,spac,spar,spat,spaw,spea,spec,spee,spel,spen,sphe,spic,spid,spik,spin,spir,spli,spoi,spon,spoo,spor,spot,spra,spre,spri,spy,squa,sque,squi,stab,stad,staf,stag,stai,stam,stan,star,stat,stay,stea,stee,stem,step,ster,stic,stil,stin,stoc,stom,ston,stoo,stor,stov,stra,stre,stri,stro,stru,stud,stuf,stum,styl,subj,subm,subw,succ,such,sudd,suff,suga,sugg,suit,summ,sun,sunn,suns,supe,supp,supr,sure,surf,surg,surp,surr,surv,susp,sust,swal,swam,swap,swar,swea,swee,swif,swim,swin,swit,swor,symb,symp,syru,syst,tabl,tack,tag,tail,tale,talk,tank,tape,targ,task,tast,tatt,taxi,teac,team,tell,ten,tena,tenn,tent,term,test,text,than,that,them,then,theo,ther,they,thin,this,thou,thre,thri,thro,thum,thun,tick,tide,tige,tilt,timb,time,tiny,tip,tire,tiss,titl,toas,toba,toda,todd,toe,toge,toil,toke,toma,tomo,tone,tong,toni,tool,toot,top,topi,topp,torc,torn,tort,toss,tota,tour,towa,towe,town,toy,trac,trad,traf,trag,trai,tran,trap,tras,trav,tray,trea,tree,tren,tria,trib,tric,trig,trim,trip,trop,trou,truc,true,trul,trum,trus,trut,try,tube,tuit,tumb,tuna,tunn,turk,turn,turt,twel,twen,twic,twin,twis,two,type,typi,ugly,umbr,unab,unaw,uncl,unco,unde,undo,unfa,unfo,unha,unif,uniq,unit,univ,unkn,unlo,unti,unus,unve,upda,upgr,upho,upon,uppe,upse,urba,urge,usag,use,used,usef,usel,usua,util,vaca,vacu,vagu,vali,vall,valv,van,vani,vapo,vari,vast,vaul,vehi,velv,vend,vent,venu,verb,veri,vers,very,vess,vete,viab,vibr,vici,vict,vide,view,vill,vint,viol,virt,viru,visa,visi,visu,vita,vivi,voca,voic,void,volc,volu,vote,voya,wage,wago,wait,walk,wall,waln,want,warf,warm,warr,wash,wasp,wast,wate,wave,way,weal,weap,wear,weas,weat,web,wedd,week,weir,welc,west,wet,whal,what,whea,whee,when,wher,whip,whis,wide,widt,wife,wild,will,win,wind,wine,wing,wink,winn,wint,wire,wisd,wise,wish,witn,wolf,woma,wond,wood,wool,word,work,worl,worr,wort,wrap,wrec,wres,wris,writ,wron,yard,year,yell,you,youn,yout,zebr,zero,zone,zoo,";

        internal static readonly ushort[] WordMap = GetWordMap();

        public static Word ParseWord(ReadOnlySpan<char> chars)
        {
            var code = Code(chars);
            var wordCode = WordMap[code];
            if (wordCode == 0) return Word.Unknown;
            return (Word)(wordCode - 1);
        }

        internal static int Code(ReadOnlySpan<char> chars)
        {
            var code = 0;
            if (chars.Length < 3) return 0;
            var maxLength = math.min(chars.Length, 4);

            for (var i = 0; i < maxLength; i++)
            {
                var c = chars[i];
                if (!char.IsLetter(c)) return 0;
                c = char.ToLowerInvariant(c);
                code *= 27;
                code += c - 'a' + 1;
            }
            return code;
        }

        internal static ushort[] GetWordMap()
        {
            var reverseWordMap = new ushort[27 * 27 * 27 * 27];

            ReadOnlySpan<char> sw = shortWords;
            var start = 0;
            ushort wordCount = 0;
            for (var i = 0; i < sw.Length; i++)
            {
                var c = sw[i];
                if (c == ',')
                {
                    var code = Code(sw.Slice(start, i - start));
                    reverseWordMap[code] = ++wordCount;
                    start = ++i;
                }
            }
            return reverseWordMap;
        }
    }
}
