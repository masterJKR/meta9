package com.example.demo.repogitory;

import com.example.demo.entity.TestJson;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface TestJsonRepo extends JpaRepository<TestJson, Integer> {


}
