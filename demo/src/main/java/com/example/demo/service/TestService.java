package com.example.demo.service;

import com.example.demo.entity.ArrowData;
import com.example.demo.entity.TestJson;
import com.example.demo.repogitory.ArrowDataRepo;
import com.example.demo.repogitory.TestJsonRepo;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.Map;

@Service
public class TestService {
    @Autowired
    private TestJsonRepo testJsonRepo;
    @Autowired
    private ArrowDataRepo arrowDataRepo;

    public List<TestJson> findAll(){
        return testJsonRepo.findAll();
    }

    public void arrowSave(Map<String, Object> map) {
        ArrowData arrowData = new ArrowData();
        arrowData.setItem((String) map.get("item"));
        arrowData.setCount((Integer) map.get("count"));
        //테이블에 저장
        arrowDataRepo.save(arrowData);

    }

    public ArrowData getArrow() {

        //Find는  select 쿼리문이다.
        // findById는  select * from 테이블명 where id = 1;
        //  primary key 지정된 컬럼으로 조회하는 쿼리문 실행 시켜준다.
        ArrowData arrowData = arrowDataRepo.findById(1L).get();
        return arrowData;
    }
}
